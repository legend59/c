//* DLL 부분 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDll
{
    public class Class1
    {
        public void swap(ref int num1, ref int num2)
        {
            int temp = num1;
            num1 = num2;
            num2 = temp;
        }
    }
}

/*
*/
* DLL 사용
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace TestAppUsingDll
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        delegate void TheMethodDelegate(ref int a, ref int b);
        private void button1_Click(object sender, EventArgs e)
        {
            //http://whiteberry.egloos.com/viewer/1953059
            Assembly u = Assembly.LoadFile(@"C:\Users\Administrator\Documents\Visual Studio 2013\Projects\TestDll\TestDll\bin\Debug\TestDll.dll");
            Module[] modules = u.GetModules();
            Type t = modules[0].GetType("TestDll.Class1");
            MethodInfo minfo = t.GetMethod("swap");
            //http://msdn.microsoft.com/ko-kr/library/53cz7sc6(v=vs.110).aspx
            TheMethodDelegate testMethod = (TheMethodDelegate)Delegate.CreateDelegate(typeof(TheMethodDelegate), minfo);
            int a = 10;
            int b = 11;
            testMethod(ref a, ref b);
        }
    }
}
[출처] DLL 동적 사용|작성자 멈춰라

