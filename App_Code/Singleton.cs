namespace EZGoal
{
    /// <summary>
    /// 懒汉模式，实质为延迟实例化
    /// </summary>
    public sealed class Singleton0
    {
        private static Singleton0 _instance = null;
        private static readonly object @lock = new object();

        private Singleton0()
        {
        }

        public static Singleton0 Instance
        {
            get
            {
                if (_instance == null)
                { // 如没有此判断也可以，但有效率下降
                    lock (@lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Singleton0();
                        }
                    }
                }
                return _instance;
            }
        }
    }

    /// <summary>
    /// 饿汉模式，通过静态初始化实现
    /// </summary>
    public sealed class Singleton1
    {
        private static readonly Singleton1 _instance = null;

        static Singleton1()
        {
            _instance = new Singleton1();
        }

        private Singleton1() { }

        public static Singleton1 Instance
        {
            get { return _instance; }
        }
    }

    /// <summary>
    /// 内部类
    /// </summary>
    public sealed class Singleton2
    {
        private Singleton2() { }

        public static Singleton2 Instance
        {
            get { return Nested.instance; }
        }

        private class Nested
        {
            internal static readonly Singleton2 instance = null;
            static Nested()
            {
                instance = new Singleton2();
            }
        }
    }
}