using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using DynamicTypeDescriptor;
using System.ComponentModel.Design;
using System.Resources;
using System.Collections;
using System.Collections.ObjectModel;

namespace DynamicTypeDescriptorApp
{

  
  [EnumResource("DynamicTypeDescriptorApp.Properties.Resources")]
  [Editor(typeof(StandardValueEditor), typeof(UITypeEditor))]
  [Flags]
  public enum Days
  {
    [StandardValue("Not Selected", Description = "Event will not reoccure.")]
    None = 0,

    [StandardValue("Monday", Description = "Day of the Moon.")]
    Mon = 1,

    [StandardValue("Tuesday", Description = "Day of Mars.")]
    Tue = 2,

    [StandardValue("Wednesday", Description = "Day of Mercury.")]
    Wed = 4,

    [StandardValue("Thursday", Description = "Day of Jupiter.")]
    Thr = 8,

    [StandardValue("Friday", Description = "Venus's day.")]
    Fri = 16,

    [StandardValue("Saturday", Description = "Day of Saturn.", Visible = false)]  // hide this one, just for the sake of it
    Sat = 32,

    [StandardValue("Sunday", Description = "Day of the sun.", Visible = false)]   // hide this one, just for the sake of it
    Sun = 64,

    [StandardValue("Weekdays", Description = "All days except Saturday and Sunday.")]
    Work = Days.Mon | Days.Tue | Days.Wed | Days.Thr | Days.Fri,

    [StandardValue("Weekend", Description = "Only Saturday and Sunday.")]
    NoWork = Days.Sat | Days.Sun,
  }

  [EnumResource("DynamicTypeDescriptorApp.Properties.Resources")]
  [Editor(typeof(StandardValueEditor), typeof(UITypeEditor))]
  public enum Placement
  {
    [StandardValue("Not Selected", "Placement has not been decided.")]
    None,

    [StandardValue("First place", "Gold medalist.")]
    One,

    [StandardValue("Second place", "Silver medalist.")]
    Two,

    [StandardValue("Third place", "Bronz medalist.")]
    Third,
  }

  [ClassResource(BaseName = "DynamicTypeDescriptorApp.Properties.Resources", KeyPrefix="TestClass_")]
  public class TestClass
  {
    public DynamicCustomTypeDescriptor m_dctd = null;

    private Image m_img = DynamicTypeDescriptorApp.Properties.Resources.ErrorState1;
    private Days m_WeekendWork = Days.Sat;

    public TestClass()
    {
      m_dctd = ProviderInstaller.Install(this);

      PropG = 1;
      PropH = 2;
      CreateOnTheFlyPropertyE( );
      CustomPropertyDescriptor cpd = m_dctd.GetProperty("PropI");
      PopululateDropDownListFromDatabaseSource(cpd);

      cpd = m_dctd.GetProperty("PropJ");
      PopululateDropDownListFromDatabaseSource(cpd);

    }

    // enum without flag attribute
    private Placement m_PropertyA = Placement.Two;
    [DisplayName("PropertyA")]
    [Category("Enumeration")]
    [Description("Description of PropertyA")]
    [Id(1, 1)]
    [DefaultValue(Placement.Two)]
    public Placement PropA
    {
      get
      {
        return m_PropertyA;
      }
      set
      {
        m_PropertyA = value;
      }
    }

    // enum with flag attribute
    private Days m_PropertyB = Days.Mon | Days.Tue;
    [DisplayName("PropertyB")]
    [Category("Enumeration")]
    [Description("Description of PropertyB")]
    [Id(2, 1)]
    [DefaultValue(Days.Mon | Days.Tue)]
    public Days PropB
    {
      get
      {
        return m_PropertyB;
      }
      set
      {
        m_PropertyB = value;
      }
    }

    // boolean as enum
    private bool m_PropertyC = true;
    [DisplayName("PropertyC")]
    [Category("Enumeration")]
    [Description("Description of PropertyC")]
    [DefaultValue(true)]
    [Id(3, 1)]
    [Editor(typeof(StandardValueEditor), typeof(UITypeEditor))]
    public bool PropC
    {
      get
      {
        return m_PropertyC;
      }
      set
      {
        m_PropertyC = value;
      }
    }

    private System.Windows.Forms.FormStartPosition m_PropertyD = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
    [DisplayName("PropertyD")]
    [Category("Enumeration")]
    [Description("Description of PropertyD")]
    [Id(4, 1)]
    [Editor(typeof(StandardValueEditor), typeof(UITypeEditor))]
    public System.Windows.Forms.FormStartPosition PropD
    {
      get
      {
        return m_PropertyD;
      }
      set
      {
        m_PropertyD = value;
      }
    }
    // on-the-fly
    private void CreateOnTheFlyPropertyE()
    {
      CustomPropertyDescriptor cpd = m_dctd.CreateProperty("PropE", typeof(int), 5,
                                            -1, // insert at the end of the list
                                            new BrowsableAttribute(true),
                                            new DefaultValueAttribute(5),
                                            new DisplayNameAttribute("PropertyE"),
                                            new DescriptionAttribute("Description of PropertyE"),
                                            new IdAttribute(5, 2));
      cpd.AddValueChanged(this, new EventHandler(this.OnTheFlyPropertyChanged));
    }

    // disable-property
    private int m_PropertyF = 80;
    [DisplayName("PropertyF")]
    [Category("Enumeration")]
    [Description("Description of PropertyF")]
    [Id(6, 2)]
    [DefaultValue(80)]
    [ReadOnly(true)]
    public int PropF
    {
      get
      {
        return m_PropertyF;
      }
      set
      {
        m_PropertyF = value;
      }
    }

    // value image
    private int m_PropertyG = 1;
    [DisplayName("PropertyG")]
    [Category("Enumeration")]
    [Description("Description of PropertyG")]
    [Id(7, 3)]
    [Editor(typeof(PropertyValuePaintEditor), typeof(UITypeEditor))]
    public int PropG
    {
      get
      {
        return m_PropertyG;
      }
      set
      {
        m_PropertyG = value;
        if (m_PropertyG < 0)
        {
          m_PropertyG = 1;
        }
        if (m_PropertyG > 3)
        {
          m_PropertyG = 3;
        }
        CustomPropertyDescriptor cpd = m_dctd.GetProperty("PropG");
        cpd.ValueImage = null;
        switch (m_PropertyG)
        {
          case 1:
            cpd.ValueImage = DynamicTypeDescriptorApp.Properties.Resources.HappyFace;
            break;
          case 2:
            cpd.ValueImage = DynamicTypeDescriptorApp.Properties.Resources.OkFace;
            break;
          case 3:
            cpd.ValueImage = DynamicTypeDescriptorApp.Properties.Resources.UnhappyFace;
            break;
        }
        TypeDescriptor.Refresh(this);
      }
    }

    // state image 
    private int m_PropertyH = 2;
    [DisplayName("PropertyH")]
    [Category("Enumeration")]
    [Description("Description of PropertyH")]
    [Id(8, 3)]
    public int PropH
    {
      get
      {
        return m_PropertyH;
      }
      set
      {
        m_PropertyH = value;
        if (m_PropertyH < 1)
        {
          m_PropertyH = 1;
        }
        if (m_PropertyH > 3)
        {
          m_PropertyH = 3;
        }
        CustomPropertyDescriptor cpd = m_dctd.GetProperty("PropH");
        cpd.StateItems.Clear( );
        for (int i = 0; i < m_PropertyH; i++)
        {
          PropertyValueUIItem pvui = new PropertyValueUIItem(DynamicTypeDescriptorApp.Properties.Resources.ErrorState1,
                                          this.UIItemClicked, "Index " + (i + 1).ToString( ) + ". Double-click the icon.");

          cpd.StateItems.Add(pvui);
        }
      }
    }

    // standard value exclusive
    private int m_PropertyI = 101;
    [DisplayName("PropertyI")]
    [Category("Enumeration")]
    [Description("Description of PropertyI")]
    [Id(9, 4)]
    [PropertyStateFlags(  PropertyFlags.Default)]
    [Editor(typeof(StandardValueEditor), typeof(UITypeEditor))]
    public int PropI
    {
      get
      {
        return m_PropertyI;
      }
      set
      {
        m_PropertyI = value;
      }
    }
    // standard value exclusive
    private int m_PropertyJ = 101;
    [DisplayName("PropertyJ")]
    [Category("Enumeration")]
    [Description("Description of PropertyJ")]
    [Id(10, 4)]
    [PropertyStateFlags(PropertyFlags.All)]
    [Editor(typeof(StandardValueEditor), typeof(UITypeEditor))]
    public int PropJ
    {
      get
      {
        return m_PropertyJ;
      }
      set
      {
        m_PropertyJ = value;
      }
    }
    private List<Size> m_PropK = new List<Size>( );
    [PropertyStateFlags((PropertyFlags.Default | PropertyFlags.ExpandIEnumerable) & ~PropertyFlags.SupportStandardValues)]
    [Id(11, 5)]
    public List<Size> PropK
    {
      get
      {
        if (m_PropK.Count == 0)
        {
          m_PropK.Add(new Size(1, 2));
          m_PropK.Add(new Size(3, 4));
          m_PropK.Add(new Size(5, 6));
        }
        return m_PropK;
      }
      set
      {
        m_PropK = value;
      }
    }
    private int[] m_PropL = new int[] { 2, 4, 6, 8 };
    [PropertyStateFlags((PropertyFlags.Default | PropertyFlags.ExpandIEnumerable) & ~PropertyFlags.SupportStandardValues)]
    [Id(13, 5)]
    public int[] PropL
    {
      get
      {
        return m_PropL;
      }
      set
      {
        m_PropL = value;
      }
    }
    private Collection<Point> m_PropM = new Collection<Point>( );
    [PropertyStateFlags((PropertyFlags.Default | PropertyFlags.ExpandIEnumerable) & ~PropertyFlags.SupportStandardValues)]
    [Id(14, 5)]
    public Collection<Point> PropM
    {
      get
      {
        if (m_PropM.Count == 0)
        {
          m_PropM.Add(new Point(11, 12));
          m_PropM.Add(new Point(13, 14));
          m_PropM.Add(new Point(15, 16));
        }
        return m_PropM;
      }
      set
      {
        m_PropM = value;
      }
    }

    private void OnTheFlyPropertyChanged( object sender, EventArgs e )
    {
      CustomPropertyDescriptor cpd = (CustomPropertyDescriptor)sender;
      if (cpd.Name == "PropE")
      {
        m_WeekendWork = (Days)Enum.ToObject(typeof(Days), cpd.GetValue(this));
      }
    }

    private void UIItemClicked( ITypeDescriptorContext context, PropertyDescriptor propDesc, PropertyValueUIItem item )
    {
      StringBuilder sb = new StringBuilder( );
      CustomPropertyDescriptor cpd = propDesc as CustomPropertyDescriptor;
      sb.AppendLine("Prop state icon clicked for property '" + cpd.DisplayName + "'.");
      sb.AppendLine("Tool tip:");
      sb.AppendLine(item.ToolTip);
      MessageBox.Show(sb.ToString( ));
    }
    private void PopululateDropDownListFromDatabaseSource( CustomPropertyDescriptor cpd )
    {
      // we actually don't have any data source in this sample.
      // we will simply add some hard coded data here.
      // we will make  customer names and id.

      if (cpd.StatandardValues.IsReadOnly ) // we cannot modifiy standard values for enum type
      {
        return;
      }

      cpd.StatandardValues.Clear( );
      string[] arrNames = { "Adam", "Brian", "Russel", "Jones", "Jakob" };
      for (int i = 101; i < 106; i++)
      {

        StandardValueAttribute sva = new StandardValueAttribute( i, arrNames[i - 101]);
        sva.Description = "Description of " + sva.DisplayName + ".";
        cpd.StatandardValues.Add(sva);

      }
    }
    public ISite GetCustomSite()
    {
      if (m_dctd == null)
      {
        return null;
      }
      return m_dctd.GetSite( );
    }

  }


  
}
