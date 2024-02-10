using System;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Http.Connections.Client;

Assembly assembly = Assembly.LoadFrom("./auth.dll");

void getMapValue(int index) {
  String cname = "\u000f\u0010\u0002";
  Type t = assembly.GetType(cname);
  if (t == null) {
    Console.WriteLine("type not found");
    Environment.Exit(0);
  }
  
  Console.WriteLine("type found");
  MethodInfo method = null;
  foreach (MethodInfo m in t.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)) {
    ParameterInfo[] pms = m.GetParameters();
    if (pms.Length == 1 && pms[0].ParameterType == typeof(int)) {
      method = m;
    }
  }
  if (method == null) {
    Console.WriteLine("method (int) not found");
    Environment.Exit(0);
  }
  Console.WriteLine("method found");
  
  Console.WriteLine("value: " + method.Invoke(null, new object[] { index }));
}

void getValue() {
  String cname = "\u0005\u0015\u0002";
  Type t = assembly.GetType(cname);
  if (t == null) {
    Console.WriteLine("type not found");
    Environment.Exit(0);
  }
  
  Console.WriteLine("type found");
  MethodInfo method = null;
  foreach (MethodInfo m in t.GetMethods(BindingFlags.Static | BindingFlags.Public)) {
    if (m.ReturnType == typeof(Stream)) {
      method = m;
    }
  }
  if (method == null) {
    Console.WriteLine("method (int) not found");
    Environment.Exit(0);
  }
  Console.WriteLine("method found");
  
  //HttpConnectionOptions opts = new HttpConnectionOptions();
  Stream SS = (Stream) method.Invoke(null, new object[] {});
  StreamReader reader = new StreamReader(SS);
  string MSG = reader.ReadLine();
  byte[] ba = Encoding.Default.GetBytes(MSG);
  Console.WriteLine(BitConverter.ToString(ba).Replace("-", ""));
}
getMapValue(-1506375197);
