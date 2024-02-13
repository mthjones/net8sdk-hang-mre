namespace net8sdk_hang_mre {
    public static class Program {
        public static void Main( string[] args ) {
            switch( args[0] ) {
                case "1": Case1(); break;
                case "2": Case2(); break;
                case "3": Case3(); break;
                case "4": Case4(); break;
                default:
                    throw new System.Exception( $"Unknown case: {args[0]}" );
            }
        }

        private static void Case1() {
            IFoo<IBaseParams> foo = new FooBase();
            foo.DoSomething();
        }

        private static void Case2() {
            IFoo<IBaseParams> foo = new Foo<IParams>();
            foo.DoSomething();
        }

        private static void Case3() {
            IFoo<IParams> foo = new Foo<IParams>();
            foo.DoSomething();
        }

        private static void Case4() {
            IFoo<IBaseParams> foo = new Foo<IBaseParams>();
            foo.DoSomething();
        }
    }

    public interface IFoo<T> {
        bool DoSomething();
    }

    public interface IBaseParams { }
    public interface IParams : IBaseParams { }

    public class FooBase : IFoo<IBaseParams> {
        bool IFoo<IBaseParams>.DoSomething() {
            return false;
        }
    }

    public sealed class Foo<T> : FooBase, IFoo<T>
        where T : IBaseParams
    {
        bool IFoo<T>.DoSomething() {
            IFoo<IBaseParams> @this = this;
            return @this.DoSomething();
        }
    }
}
