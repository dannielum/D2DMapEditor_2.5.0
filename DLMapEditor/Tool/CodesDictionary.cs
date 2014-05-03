using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace D2DMapEditor
{
    class CodesDictionary
    {
        public static Boolean IsValidNameFormat(string name)
        {
            if (name.Length > 100)
                return false;

            Regex regex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);
            Match match = regex.Match(name);

            if (!match.Success)
                return false;
            else
                return true;
        }

        public static Boolean IsValidName(string name, ProgrammingLanguage language)
        {
            if (!IsValidNameFormat(name))
                return false;

            switch (language)
            {
                case ProgrammingLanguage.CPP:
                    return IsValidCPPWord(name);
                case ProgrammingLanguage.CSharp:
                    return IsValidCSharpWord(name);
                case ProgrammingLanguage.ActionScript:
                    return IsValidASWord(name);
                case ProgrammingLanguage.XML:
                    return IsValidXMLWord(name);
                case ProgrammingLanguage.XMLLite:
                    return IsValidXMLLiteWord(name);
                default: 
                    return true;
            }
        }

        private static Boolean IsValidCPPWord(string name)
        {
            #region C++ Reserved Words
            List<string> CPPReservedWords = new List<string>();
            CPPReservedWords.Add("auto");
            CPPReservedWords.Add("const");
            CPPReservedWords.Add("double");
            CPPReservedWords.Add("float");
            CPPReservedWords.Add("int");
            CPPReservedWords.Add("short");
            CPPReservedWords.Add("struct");
            CPPReservedWords.Add("unsigned");
            CPPReservedWords.Add("break");
            CPPReservedWords.Add("continue");
            CPPReservedWords.Add("else");
            CPPReservedWords.Add("for");
            CPPReservedWords.Add("long");
            CPPReservedWords.Add("signed");
            CPPReservedWords.Add("switch");
            CPPReservedWords.Add("void");
            CPPReservedWords.Add("case");
            CPPReservedWords.Add("default");
            CPPReservedWords.Add("enum");
            CPPReservedWords.Add("goto");
            CPPReservedWords.Add("register");
            CPPReservedWords.Add("sizeof");
            CPPReservedWords.Add("typedef");
            CPPReservedWords.Add("volatile");
            CPPReservedWords.Add("char");
            CPPReservedWords.Add("do");
            CPPReservedWords.Add("extern");
            CPPReservedWords.Add("if");
            CPPReservedWords.Add("return");
            CPPReservedWords.Add("static");
            CPPReservedWords.Add("union");
            CPPReservedWords.Add("while");
            CPPReservedWords.Add("asm");
            CPPReservedWords.Add("dynamic_cast");
            CPPReservedWords.Add("reinterpret_cast");
            CPPReservedWords.Add("namespace");
            CPPReservedWords.Add("try");
            CPPReservedWords.Add("bool");
            CPPReservedWords.Add("explicit");
            CPPReservedWords.Add("new");
            CPPReservedWords.Add("static_cast");
            CPPReservedWords.Add("typeid");
            CPPReservedWords.Add("catch");
            CPPReservedWords.Add("false");
            CPPReservedWords.Add("operator");
            CPPReservedWords.Add("template");
            CPPReservedWords.Add("typename");
            CPPReservedWords.Add("class");
            CPPReservedWords.Add("friend");
            CPPReservedWords.Add("private");
            CPPReservedWords.Add("this");
            CPPReservedWords.Add("using");
            CPPReservedWords.Add("const_cast");
            CPPReservedWords.Add("inline");
            CPPReservedWords.Add("public");
            CPPReservedWords.Add("throw");
            CPPReservedWords.Add("virtual");
            CPPReservedWords.Add("delete");
            CPPReservedWords.Add("mutable");
            CPPReservedWords.Add("protected");
            CPPReservedWords.Add("true");
            CPPReservedWords.Add("wchar_t");
            CPPReservedWords.Add("and");
            CPPReservedWords.Add("bitand");
            CPPReservedWords.Add("compl");
            CPPReservedWords.Add("not_eq");
            CPPReservedWords.Add("or_eq");
            CPPReservedWords.Add("xor_eq");
            CPPReservedWords.Add("and_eq");
            CPPReservedWords.Add("bitor");
            CPPReservedWords.Add("not");
            CPPReservedWords.Add("or");
            CPPReservedWords.Add("xor");                                   
            CPPReservedWords.Add("cin");
            CPPReservedWords.Add("endl");
            CPPReservedWords.Add("INT_MIN");
            CPPReservedWords.Add("iomanip");
            CPPReservedWords.Add("main");
            CPPReservedWords.Add("npos");
            CPPReservedWords.Add("std");
            CPPReservedWords.Add("cout");
            CPPReservedWords.Add("include");
            CPPReservedWords.Add("INT_MAX");
            CPPReservedWords.Add("iostream");
            CPPReservedWords.Add("MAX_RAND");
            CPPReservedWords.Add("NULL");
            CPPReservedWords.Add("string");
            #endregion

            if (CPPReservedWords.IndexOf(name) > 0)
                return false;
            else
                return true;
        }

        private static Boolean IsValidCSharpWord(string name)
        {
            #region C# Reserved Words
            List<string> CSharpReservedWords = new List<string>();
            CSharpReservedWords.Add("abstract");
            CSharpReservedWords.Add("event");
            CSharpReservedWords.Add("new");
            CSharpReservedWords.Add("struct");
            CSharpReservedWords.Add("as");
            CSharpReservedWords.Add("explicit");
            CSharpReservedWords.Add("null");
            CSharpReservedWords.Add("switch");
            CSharpReservedWords.Add("base");
            CSharpReservedWords.Add("extern");
            CSharpReservedWords.Add("object");
            CSharpReservedWords.Add("this");
            CSharpReservedWords.Add("bool");
            CSharpReservedWords.Add("false");
            CSharpReservedWords.Add("operator");
            CSharpReservedWords.Add("throw");
            CSharpReservedWords.Add("break");
            CSharpReservedWords.Add("finally");
            CSharpReservedWords.Add("out");
            CSharpReservedWords.Add("true");
            CSharpReservedWords.Add("byte");
            CSharpReservedWords.Add("fixed");
            CSharpReservedWords.Add("override");
            CSharpReservedWords.Add("try");
            CSharpReservedWords.Add("case");
            CSharpReservedWords.Add("float");
            CSharpReservedWords.Add("params");
            CSharpReservedWords.Add("typeof");
            CSharpReservedWords.Add("catch");
            CSharpReservedWords.Add("for");
            CSharpReservedWords.Add("private");
            CSharpReservedWords.Add("uint");
            CSharpReservedWords.Add("char");
            CSharpReservedWords.Add("foreach");
            CSharpReservedWords.Add("protected");
            CSharpReservedWords.Add("ulong");
            CSharpReservedWords.Add("checked");
            CSharpReservedWords.Add("goto");
            CSharpReservedWords.Add("public");
            CSharpReservedWords.Add("unchecked");
            CSharpReservedWords.Add("class");
            CSharpReservedWords.Add("if");
            CSharpReservedWords.Add("readonly");
            CSharpReservedWords.Add("unsafe");
            CSharpReservedWords.Add("const");
            CSharpReservedWords.Add("implicit");
            CSharpReservedWords.Add("ref");
            CSharpReservedWords.Add("ushort");
            CSharpReservedWords.Add("continue");
            CSharpReservedWords.Add("in");
            CSharpReservedWords.Add("return");
            CSharpReservedWords.Add("using");
            CSharpReservedWords.Add("decimal");
            CSharpReservedWords.Add("int");
            CSharpReservedWords.Add("sbyte");
            CSharpReservedWords.Add("virtual");
            CSharpReservedWords.Add("default");
            CSharpReservedWords.Add("interface");
            CSharpReservedWords.Add("sealed");
            CSharpReservedWords.Add("volatile");
            CSharpReservedWords.Add("delegate");
            CSharpReservedWords.Add("internal");
            CSharpReservedWords.Add("short");
            CSharpReservedWords.Add("void");
            CSharpReservedWords.Add("do");
            CSharpReservedWords.Add("is");
            CSharpReservedWords.Add("sizeof");
            CSharpReservedWords.Add("while");
            CSharpReservedWords.Add("double");
            CSharpReservedWords.Add("lock");
            CSharpReservedWords.Add("stackalloc");
            CSharpReservedWords.Add("else");
            CSharpReservedWords.Add("long");
            CSharpReservedWords.Add("static");
            CSharpReservedWords.Add("enum");
            CSharpReservedWords.Add("namespace");
            CSharpReservedWords.Add("string");
            CSharpReservedWords.Add("from");
            CSharpReservedWords.Add("get");
            CSharpReservedWords.Add("group");
            CSharpReservedWords.Add("into");
            CSharpReservedWords.Add("join");
            CSharpReservedWords.Add("let");
            CSharpReservedWords.Add("orderby");
            CSharpReservedWords.Add("partial");
            CSharpReservedWords.Add("select");
            CSharpReservedWords.Add("set");
            CSharpReservedWords.Add("value");
            CSharpReservedWords.Add("var");
            CSharpReservedWords.Add("where");
            CSharpReservedWords.Add("yield");
            #endregion
            
            if (CSharpReservedWords.IndexOf(name) > 0)
                return false;
            else
                return true;
        }

        private static Boolean IsValidASWord(string name)
        {
            #region ActionScript Reserved Words
            List<string> ASReservedWords = new List<string>();
            ASReservedWords.Add("add");
            ASReservedWords.Add("for");
            ASReservedWords.Add("lt");
            ASReservedWords.Add("tellTarget");
            ASReservedWords.Add("and");
            ASReservedWords.Add("function");
            ASReservedWords.Add("ne");
            ASReservedWords.Add("this");
            ASReservedWords.Add("break");
            ASReservedWords.Add("ge");
            ASReservedWords.Add("new");
            ASReservedWords.Add("typeof");
            ASReservedWords.Add("continue");
            ASReservedWords.Add("gt");
            ASReservedWords.Add("not");
            ASReservedWords.Add("var");
            ASReservedWords.Add("delete");
            ASReservedWords.Add("if");
            ASReservedWords.Add("on");
            ASReservedWords.Add("void");
            ASReservedWords.Add("do");
            ASReservedWords.Add("ifFrameLoaded");
            ASReservedWords.Add("onClipEvent");
            ASReservedWords.Add("while");
            ASReservedWords.Add("else");
            ASReservedWords.Add("in");
            ASReservedWords.Add("or");
            ASReservedWords.Add("with");
            ASReservedWords.Add("eq");
            ASReservedWords.Add("le");
            ASReservedWords.Add("return");
            ASReservedWords.Add("abstract");
            ASReservedWords.Add("extends");
            ASReservedWords.Add("private");
            ASReservedWords.Add("boolean");
            ASReservedWords.Add("final");
            ASReservedWords.Add("protected");
            ASReservedWords.Add("byte");
            ASReservedWords.Add("finally");
            ASReservedWords.Add("public");
            ASReservedWords.Add("case");
            ASReservedWords.Add("float");
            ASReservedWords.Add("short");
            ASReservedWords.Add("catch");
            ASReservedWords.Add("goto");
            ASReservedWords.Add("static");
            ASReservedWords.Add("char");
            ASReservedWords.Add("implements");
            ASReservedWords.Add("super");
            ASReservedWords.Add("class");
            ASReservedWords.Add("import");
            ASReservedWords.Add("switch");
            ASReservedWords.Add("const");
            ASReservedWords.Add("instanceof");
            ASReservedWords.Add("synchronized");
            ASReservedWords.Add("debugger");
            ASReservedWords.Add("int");
            ASReservedWords.Add("throws");
            ASReservedWords.Add("default");
            ASReservedWords.Add("interface");
            ASReservedWords.Add("transient");
            ASReservedWords.Add("double");
            ASReservedWords.Add("long");
            ASReservedWords.Add("try");
            ASReservedWords.Add("enum");
            ASReservedWords.Add("native");
            ASReservedWords.Add("volatile");
            ASReservedWords.Add("export");
            ASReservedWords.Add("package");
            #endregion

            if (ASReservedWords.IndexOf(name) > 0)
                return false;
            else
                return true;
        }

        private static Boolean IsValidXMLWord(string name)
        {
            return true;
        }

        private static Boolean IsValidXMLLiteWord(string name)
        {
            return true;
        }
    }
}
