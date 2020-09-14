using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using DynamicTypeDescriptor;
using System.Resources;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.ObjectModel;

namespace DynamicTypeDescriptorApp
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent( );
    }

    private void Form1_Load( object sender, EventArgs e )
    {

      TestClass tc = new TestClass( );
      this.propertyGrid1.Site = tc.GetCustomSite( ); // this is needed for property state icons
      propertyGrid1.SelectedObject = tc;

      foreach (object o in Enum.GetValues(typeof(PropertySort)))
      {
        this.cboSortByGrid.Items.Add(o);
      }
      foreach (object o in Enum.GetValues(typeof(CustomSortOrder)))
      {
        this.cboSortByProperty.Items.Add(o);
      }
      foreach (object o in Enum.GetValues(typeof(CustomSortOrder)))
      {
        this.cboSortByCategory.Items.Add(o);
      }

      this.cboSortByGrid.SelectedIndex = (int)PropertySort.Categorized;
      this.cboSortByProperty.SelectedIndex = (int)CustomSortOrder.AscendingById;
      this.cboSortByCategory.SelectedIndex = (int)CustomSortOrder.AscendingById;

      this.cboLang.SelectedIndex = 0;

      this.propertyGrid2.SelectedObject = new PropertyWrapper(propertyGrid1.SelectedObject);

    }

    private void cboSortBy_SelectedIndexChanged( object sender, EventArgs e )
    {
      propertyGrid1.PropertySort = (PropertySort)Enum.ToObject(typeof(PropertySort), cboSortByGrid.SelectedItem);
      TypeDescriptor.Refresh(propertyGrid1.SelectedObject);

    }

    private void cboSortByProperty_SelectedIndexChanged( object sender, EventArgs e )
    {
      TestClass tc = propertyGrid1.SelectedObject as TestClass;
      tc.m_dctd.PropertySortOrder = (CustomSortOrder)Enum.ToObject(typeof(CustomSortOrder), cboSortByProperty.SelectedItem);
      TypeDescriptor.Refresh(propertyGrid1.SelectedObject);
    }

    private void cboSortByCategory_SelectedIndexChanged( object sender, EventArgs e )
    {
      TestClass tc = propertyGrid1.SelectedObject as TestClass;
      tc.m_dctd.CategorySortOrder = (CustomSortOrder)Enum.ToObject(typeof(CustomSortOrder), cboSortByCategory.SelectedItem);
      TypeDescriptor.Refresh(propertyGrid1.SelectedObject);
    }

    private void cboLang_SelectedIndexChanged( object sender, EventArgs e )
    {
      switch (cboLang.SelectedIndex)
      {
        case 0:
          Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
          break;
        case 1:
          Thread.CurrentThread.CurrentUICulture = new CultureInfo("da-DK");
          break;
        case 2:
          Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar");
          break;
      }
      TypeDescriptor.Refresh(propertyGrid1.SelectedObject);
    }

    private void ctxReset_Opening( object sender, CancelEventArgs e )
    {
      mnuReset.Enabled = true;
      if (propertyGrid1.SelectedGridItem == null)
      {
        mnuReset.Enabled = false;
      }
      else if (propertyGrid1.SelectedGridItem.PropertyDescriptor.CanResetValue(propertyGrid1.SelectedObject) == false)
      {
        mnuReset.Enabled = false;
      }
    }

    private void mnuReset_Click( object sender, EventArgs e )
    {
      propertyGrid1.SelectedGridItem.PropertyDescriptor.ResetValue(propertyGrid1.SelectedObject);
      TypeDescriptor.Refresh(propertyGrid1.SelectedObject);

    }

    private void propertyGrid1_SelectedGridItemChanged( object sender, SelectedGridItemChangedEventArgs e )
    {
      PropertyWrapper pw = propertyGrid2.SelectedObject as PropertyWrapper;
      try
      {
        pw.SelectedProperty = e.NewSelection.PropertyDescriptor as CustomPropertyDescriptor;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

  }

  public class PropertyWrapper
  {
    private CustomPropertyDescriptor m_cpd = null;
    private DynamicCustomTypeDescriptor m_dctd = null;
    private object m_owner = null;
    public PropertyWrapper( object owner )
    {
      m_dctd = ProviderInstaller.Install(this);
      m_owner = owner;
      CustomPropertyDescriptor cpd = m_dctd.GetProperty("SelectedProperty");
      foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(owner))
      {
        StandardValueAttribute sva = new StandardValueAttribute(pd);
        sva.DisplayName = pd.DisplayName;
        sva.Description = pd.Description;
        cpd.StatandardValues.Add(sva);
      }
      SelectedProperty = (cpd.StatandardValues.ToArray( )[0].Value) as CustomPropertyDescriptor;
      m_dctd.CategorySortOrder = CustomSortOrder.None;
      m_dctd.PropertySortOrder = CustomSortOrder.None;

      this.PropertyFlags = m_cpd.PropertyFlags;
    }

    [Category("Step 1: Property Selection")]
    [DisplayName("Select a property")]
    [Description("Select a property to modify its property.")]
    [Editor(typeof(StandardValueEditor), typeof(UITypeEditor))]
    public CustomPropertyDescriptor SelectedProperty
    {
      get
      {
        return m_cpd;
      }
      set
      {
        m_cpd = value;
        TypeDescriptor.Refresh(this);
      }

    }
    [Category("Step 2: Modify Selected Property")]
    [Description("Show/hide the selected proprty.")]
    public bool Visible
    {
      get
      {
        if (m_cpd == null)
        {
          return true;
        }
        return m_cpd.IsBrowsable;
      }
      set
      {
        m_cpd.SetIsBrowsable(value);
        TypeDescriptor.Refresh(m_owner);
      }
    }
    [Category("Step 2: Modify Selected Property")]
    [Description("Enable/disable the selected proprty.")]
    public bool Disabled
    {
      get
      {
        if (m_cpd == null)
        {
          return false;
        }
        return m_cpd.IsReadOnly;
      }
      set
      {
        m_cpd.SetIsReadOnly(value);
        TypeDescriptor.Refresh(m_owner);
      }
    }

    [Category("Step 2: Modify Selected Property")]
    [Editor(typeof(StandardValueEditor), typeof(UITypeEditor))]
    [Description("Sets/gets various flags of the property.")]
    public PropertyFlags PropertyFlags
    {
      get
      {
        if (m_cpd == null)
        {
          return PropertyFlags.All;
        }
        return m_cpd.PropertyFlags;
      }
      set
      {
        m_cpd.PropertyFlags = value;
        CustomPropertyDescriptor cpd = m_dctd.GetProperty("DisplayName");
        cpd.SetIsReadOnly((value & PropertyFlags.LocalizeDisplayName) > 0);

        cpd = m_dctd.GetProperty("Description");
        cpd.SetIsReadOnly((value & PropertyFlags.LocalizeDescription) > 0);

        cpd = m_dctd.GetProperty("Category");
        cpd.SetIsReadOnly((value & PropertyFlags.LocalizeCategoryName) > 0);
        TypeDescriptor.Refresh(this);
        TypeDescriptor.Refresh(m_owner);
      }

    }

    [Category("Step 2: Modify Selected Property")]
    [Description("Sets/gets display name. Use PropertyFlag proeprty to enable this property.")]
    public string DisplayName
    {
      get
      {
        if (m_cpd == null)
        {
          return "";
        }
        return m_cpd.DisplayName;
      }
      set
      {
        m_cpd.SetDisplayName(value);
        TypeDescriptor.Refresh(m_owner);
      }
    }

    [Category("Step 2: Modify Selected Property")]
    [Description("Sets/gets description. Use PropertyFlag proeprty to enable this property.")]
    public string Description
    {
      get
      {
        if (m_cpd == null)
        {
          return "";
        }
        return m_cpd.Description;
      }
      set
      {
        m_cpd.SetDescription(value);
      }
    }

    private string m_Category = String.Empty;
    [Category("Step 2: Modify Selected Property")]
    [Description("Sets/gets category name. Use PropertyFlag proeprty to enable this property.")]
    public string Category
    {
      get
      {
        if (m_cpd == null)
        {
          return "";
        }
        return m_cpd.Category.TrimStart('\t');
      }
      set
      {
        m_cpd.SetCategory(value);
      }
    }
    [Category("Step 1: Property Selection")]
    [Description("Shows the actual name of the property in the source code.")]
    public string ActualPropertyName
    {
      get
      {
        if (m_cpd == null)
        {
          return "";
        }
        return m_cpd.Name;
      }
    }
  }


}