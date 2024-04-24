

using MyLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;




namespace ReflectionApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filePath = Path.GetFullPath("../../../../MyLib/bin/Debug/net8.0/MyLib.dll");

            Assembly assembly = Assembly.LoadFile(filePath);
            Console.WriteLine(assembly.FullName);
            Type[] types = assembly.GetTypes();
            /* foreach(Type t in types)
            {
                Console.WriteLine(t.FullName);
                MethodInfo[] methods = t.GetMethods();
                foreach(MethodInfo m in methods)
                {
                    Console.WriteLine(m.Name + " Type:"+ m.ReturnType);
                }
                Console.WriteLine();
            }*/
            Type cc = assembly.GetType("MyLib.Controllers.CustomerController");
            object ccObj = Activator.CreateInstance(cc);
            MethodInfo listMethod = cc.GetMethod("List");
            object tmp = listMethod.Invoke(ccObj,new object[] {2});
            Console.WriteLine(tmp);

            string url = "/Customer/Add?Name=Pepa&Age=30&IsActive=true";
            string[] parts = url.Split('?');
            string[] left = parts[0].Split('/');
            string[] right = parts[1].Split('&');

            string controllerName = left[1];
            string actionName = left[2];

            Dictionary<string, string> arguments = right
                .Select(x => x.Split('='))
                .ToDictionary(x => x[0], y => y[1], StringComparer.OrdinalIgnoreCase);//Mužou se jmenovat stejně, vyrazy spolu nesouvisi
            Type controllerType = assembly.GetType("MyLib.Controllers." + controllerName + "Controller");
            object controllerObj = Activator.CreateInstance(controllerType);
            MethodInfo actionMethod = controllerType.GetMethod(actionName);
            List<object> parameters = new List<object>();
            foreach(ParameterInfo parameter in actionMethod.GetParameters())
            {
                string val = arguments[parameter.Name];
                if(parameter.ParameterType == typeof(int))//is int by nefungovalo protože ParameterType má type Type
                {
                    parameters.Add(int.Parse(val));
                }
                else if (parameter.ParameterType == typeof(string))
                {
                    parameters.Add(val);
                }
                else if(parameter.ParameterType == typeof(bool))
                {
                    parameters.Add(bool.Parse(val));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            object result = actionMethod.Invoke(controllerObj,parameters.ToArray());
            Console.WriteLine(result);

            // ---- 8
            Type customerType = assembly.GetType("MyLib.Models.Customer");
            object customerObj = Activator.CreateInstance(customerType);
            PropertyInfo nameProp = customerType.GetProperty("Name");
            nameProp.SetValue(customerObj, "Honza");

            string name = (string)nameProp.GetValue(customerObj);
            Console.WriteLine(name);
            //Pomocí reflexe se dá zavolat privátní metody a přistupovat k privátním proměnným, kinda hackerman
            foreach(FieldInfo fi in customerType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Console.WriteLine(fi.Name);
            }
            Customer c = new Customer();
            Type cType = c.GetType();
            foreach(Attribute attr in cType.GetCustomAttributes())//Na cType bychom mohli zavolat GetProperty a ziskali bychom attributy od te property
            {
                if(attr is TableNameAttribute tn)
                {
                    Console.WriteLine(tn.Name);
                }
                Console.WriteLine(attr);
            }
        }


    }
}