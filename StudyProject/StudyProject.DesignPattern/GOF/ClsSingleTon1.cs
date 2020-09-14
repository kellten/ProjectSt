namespace StudyProject.DesignPattern.GOF
{
    class ClsSingleTon1
    {
        private static ClsSingleTon1 _clsSingleTon1;
        private static object syncLock = new object();

        public string Name { get; set; }
        // 'protected' 로 생성자를 만듬
        protected ClsSingleTon1()
        {
        }

        public static ClsSingleTon1 Instance()
        {
            if (_clsSingleTon1 == null)
            {
                lock (syncLock)
                {
                    if (_clsSingleTon1 == null)
                    {
                        _clsSingleTon1 = new ClsSingleTon1();
                    }
                }
                
            }

            return _clsSingleTon1;
        }
        
    }
}