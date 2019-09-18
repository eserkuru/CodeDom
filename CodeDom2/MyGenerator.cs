using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CSharp;

namespace CodeDom2
{
    public static class MyGenerator
    {
        public static void Test()
        {

            CodeCompileUnit lUnit = new CodeCompileUnit();
            CodeNamespace lNamespace = new CodeNamespace("CodeDom2");

            lNamespace.Imports.Add(new CodeNamespaceImport("System.IO"));
            lUnit.Namespaces.Add(lNamespace);

            CodeTypeDeclaration lClass = new CodeTypeDeclaration("MyClass");
            CodeMethodInvokeExpression lExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Console"), "WriteLine", new CodePrimitiveExpression("hello world !"));
            lNamespace.Types.Add(lClass);

            // write Main entry point method
            CodeEntryPointMethod lMain = new CodeEntryPointMethod();
            lMain.Statements.Add(lExpression);
            lClass.Members.Add(lMain);

            // write another method
            CodeMemberMethod lMethod = new CodeMemberMethod();
            lMethod.Name = "DoSomething";
            lClass.Members.Add(lMethod);

            //string lDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\";
            string lSolutionPath =
                Path.GetDirectoryName(
                    Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()))) + @"\";
            CodeGeneratorOptions lOptions = new CodeGeneratorOptions();
            lOptions.IndentString = "  "; // or "\t";
            lOptions.BlankLinesBetweenMembers = true;

            // generate a C# source code file
            CSharpCodeProvider lCSharpCodeProvider = new CSharpCodeProvider();
            using (StreamWriter lStreamWriter = new StreamWriter(lSolutionPath + "HelloWorld.cs", false))
            {
                lCSharpCodeProvider.GenerateCodeFromCompileUnit(lUnit, lStreamWriter, lOptions);
            }
        }
    }
}
