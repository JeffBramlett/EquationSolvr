using EquationSolver.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class ExpressionSolver
    {
        #region Enums and Constants
        const int NONE = 0;
        const int VAR = 1;
        const int DEL = 2;
        const int NUM = 3;

        /// <summary>
        /// Enumeration of Result Types
        /// </summary>
        public enum ResultType
        {
            /// <summary>
            /// Default enum value
            /// </summary>
            NONE,
            /// <summary>
            /// ResultType as Boolean
            /// </summary>
            BOOL,
            /// <summary>
            /// ResultType as Number
            /// </summary>
            NUMBER,
            /// <summary>
            /// ResultType as String
            /// </summary>
            STRING
        }
        #endregion

        #region Fields
        private VariableProvider _varProvider;

        int _type = 0;
        char[] _expr;
        int _pos = 0;
        char[] _token;

        double _resultXXXAsDouble;
        decimal _resultAsDecimal;
        bool _bResult;
        string _strResult;

        bool _isStringCompare = false;
        bool _noOperator = false;

        /// <summary>
        /// The Result type of the expression.
        /// </summary>
        ResultType resType = ResultType.NONE;

        StringBuilder errors;
        #endregion
        
        #region Properties
        /// <summary>
        /// The enumeration type of the result.
        /// </summary>
        public ResultType TypeOfResult
        {
            get
            {
                return resType;
            }
        }

        /// <summary>
        /// Any parsing errors that may have occurred.
        /// </summary>
        public string Errors
        {
            get
            {
                return errors.ToString();
            }
        }

        /// <summary>
        /// The result as a double
        /// </summary>
        public decimal Result
        {
            get
            {
                if (resType == ResultType.BOOL)
                {
                    if (_bResult)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else if (resType == ResultType.NUMBER)
                {
                    return _resultAsDecimal;
                }
                else if (resType == ResultType.STRING)
                {
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// The boolean result of the expression
        /// </summary>
        public bool BoolResult
        {
            get
            {
                if (resType == ResultType.BOOL)
                {
                    return _bResult;
                }
                else if (resType == ResultType.NUMBER)
                {
                    if (_resultAsDecimal > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (resType == ResultType.STRING)
                {
                    if (_strResult == null || _strResult.CompareTo("") == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// The string of the result of the expression.
        /// </summary>
        /// <remarks>More need here .... </remarks>
        public string StringResult
        {
            get
            {
                if (resType == ResultType.BOOL)
                {
                    return "" + _bResult;
                }
                else if (resType == ResultType.NUMBER)
                {
                    return "" + _resultAsDecimal;
                }
                else if (resType == ResultType.STRING)
                {
                    return _strResult;
                }
                else
                {
                    return _strResult;
                }
            }
        }
        #endregion

        #region Ctors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ExpressionSolver()
        {
            _resultAsDecimal = 0;
            _bResult = false;
            _strResult = "";
        }
        #endregion

        #region "is" methods
        private bool IsWhite(char c)
        {
            if (c == ' ' || c == '\t')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsNumeric(char c)
        {
            if ((c >= '0' && c <= '9') || c == '.')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsAlpha(char c)
        {
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '_' || c == '.' || c == ',')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsMinus(char c)
        {
            if (c == '-')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsDelimiter(char c)
        {
            if (c == '+' || c == '-' || c == '*' || c == '/' ||
                c == '%' || c == '^' || c == '(' || c == ')' ||
                c == ',' || c == '=')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsComparator(char c)
        {
            if (c == '<' || c == '>' || c == '=' || c == '!')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Math functions
        private decimal ToDegrees(double x)
        {
            return Convert.ToDecimal((180.0 / Math.PI) * x);
        }
        private decimal ToRadians(double x)
        {
            return Convert.ToDecimal((Math.PI / 180.0) * x);
        }
        #endregion

        #region Evaluation functions
        /// <summary>
        /// This is the method to call to run the expression machine.
        /// </summary>
        /// <param name="input">the string that is the expression</param>
        /// <param name="variableProvider">the variable engine to get variables from</param>
        public void Resolve(string input, VariableProvider variableProvider)
        {
            _varProvider = variableProvider;

            input = input.Replace(" and ", " & ");
            input = input.Replace(" AND ", " & ");
            input = input.Replace(" And ", " & ");
            input = input.Replace(" aNd ", " & ");
            input = input.Replace(" anD ", " & ");

            input = input.Replace(" or ", " | ");
            input = input.Replace(" OR ", " | ");
            input = input.Replace(" Or ", " | ");
            input = input.Replace(" oR ", " | ");

            input = input.Replace("\r", " ");
            input = input.Replace("\n", " ");

            string sCompCheck = input.Trim();

            if (IsComparison(sCompCheck) || IsCompoundComparison(sCompCheck))
            {
                resType = ResultType.BOOL;
            }
            else if (HasOperator(sCompCheck))
            {
                resType = ResultType.NUMBER;
            }
            else
            {
                double d;
                if (Utilities.StringToDouble(input, out d))
                {
                    resType = ResultType.NUMBER;
                }
                else
                {
                    resType = ResultType.STRING;
                }
            }

            Evaluate(input);
        }

        private void Evaluate(string input)
        {
            try
            {
                _resultAsDecimal = 0;
                _pos = 0;
                _strResult = "";
                _bResult = false;
                errors = new StringBuilder();

                string e = input.Trim();

                if (!HasOperator(e))
                {
                    _noOperator = true;
                    Variable var = null;
                    string sVarAttr = string.Empty;
                    if (e.IndexOf(".") > 0)
                    {
                        int iNdx = e.IndexOf(".");
                        string sVarName = e.Substring(0, iNdx);
                        sVarAttr = e.Substring(iNdx + 1);
                        var = _varProvider[sVarName];
                    }
                    else
                    {
                        var = _varProvider[e];
                    }
                    if (var == null)
                    {
                        _expr = e.ToCharArray();
                        Parse();
                        if (_type == NUM && Utilities.IsANumber(e))
                        {
                            _resultAsDecimal = decimal.Parse(e);
                            _strResult = "" + _resultAsDecimal;
                            if (_resultAsDecimal < 0)
                            {
                                _bResult = false;
                            }
                            else
                            {
                                _bResult = true;
                            }
                        }
                        else
                        {
                            _strResult = e;
                            _resultAsDecimal = 0;
                            if (_strResult.Length > 0)
                            {
                                _bResult = true;
                            }
                            else
                            {
                                _bResult = false;
                            }
                        }
                    }
                    else
                    {
                        if (var.VariableType == VariableTypes.TEXT)
                        {
                            _isStringCompare = true;
                            _resultAsDecimal = var.DecimalValue;
                            _bResult = var.BoolValue;
                            _strResult = var.StringValue;
                        }
                        else
                        {
                            _isStringCompare = false;
                            _resultAsDecimal = var.DecimalValue;
                            _bResult = var.BoolValue;
                            _strResult = var.StringValue;
                        }
                    }
                }
                else if (IsCompoundComparison(e))
                {

                    _noOperator = false;
                    if (ProcessComparison(e))
                    {
                        _resultAsDecimal = 1;
                        _bResult = true;
                    }
                    else
                    {
                        _resultAsDecimal = 0;
                        _bResult = false;
                    }
                }
                else if (IsComparison(e))
                {

                    _noOperator = false;
                    if (Compare(e))
                    {
                        _resultAsDecimal = 1;
                        _bResult = true;
                    }
                    else
                    {
                        _resultAsDecimal = 0;
                        _bResult = false;
                    }
                }
                else
                {
                    _noOperator = false;
                    string etmp = e + "~";
                    _expr = etmp.ToCharArray();

                    if (e.IndexOf("[") > -1)
                    {
                    }
                    else
                    {
                        while (_pos < _expr.Length - 1)
                        {
                            Parse();
                            Assignment(ref _resultAsDecimal);
                        }
                    }
                }
            }
            catch (Exception re)
            {
                errors.Append("Error in Evaluate: " + re.Message + re.StackTrace);
            }
        }

        private bool IsOperator(char c)
        {
            bool bRet = false;
            string s = new string(c, 1);
            string[] ops = { "+", "-", "/", "*", "%", "|", "&" };
            foreach (string str in ops)
            {
                if (s.CompareTo(str) == 0)
                {
                    bRet = true;
                    break;
                }
            }
            return bRet;
        }
        /// <summary>
        /// Loads and resolves a comparison string
        /// </summary>
        /// <param name="input">the expression as a string</param>
        /// <returns>the result of the comparison (true or false)</returns>
        private bool Compare(string input)
        {
            try
            {
                if (input.IndexOf("<=") > 0)
                {
                    int ndx = input.IndexOf("<=");
                    string lstr = input.Substring(0, ndx);
                    string rstr = input.Substring(ndx + 2);

                    Evaluate(lstr);
                    decimal lval = _resultAsDecimal;

                    Evaluate(rstr);
                    decimal rval = _resultAsDecimal;

                    if (lval <= rval)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (input.IndexOf(">=") > 0)
                {
                    int ndx = input.IndexOf(">=");
                    string lstr = input.Substring(0, ndx);
                    string rstr = input.Substring(ndx + 2);

                    Evaluate(lstr);
                    decimal lval = _resultAsDecimal;

                    Evaluate(rstr);
                    decimal rval = _resultAsDecimal;

                    if (lval >= rval)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (input.IndexOf("!=") > 0)
                {
                    int ndx = input.IndexOf("!=");
                    string lstr = input.Substring(0, ndx);
                    string rstr = input.Substring(ndx + 2);

                    if (rstr.IndexOf("\"") > -1)
                    {
                        Variable var = _varProvider[lstr];
                        string strTrim = rstr.Trim();

                        string strSub = strTrim.Substring(1, strTrim.Length - 2);
                        if (var.StringValue.CompareTo(strSub) == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Evaluate(lstr);
                        decimal lval = _resultAsDecimal;

                        Evaluate(rstr);
                        decimal rval = _resultAsDecimal;

                        if (lval != rval)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else if (input.IndexOf("<") > 0)
                {
                    int ndx = input.IndexOf("<");
                    string lstr = input.Substring(0, ndx);
                    string rstr = input.Substring(ndx + 1);

                    Evaluate(lstr);
                    decimal lval = _resultAsDecimal;

                    Evaluate(rstr);
                    decimal rval = _resultAsDecimal;

                    if (lval < rval)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (input.IndexOf(">") > 0)
                {
                    int ndx = input.IndexOf(">");
                    string lstr = input.Substring(0, ndx);
                    string rstr = input.Substring(ndx + 1);

                    Evaluate(lstr.Trim());
                    decimal lval = _resultAsDecimal;

                    Evaluate(rstr.Trim());
                    decimal rval = _resultAsDecimal;

                    if (lval > rval)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (input.IndexOf("=") > 0)
                {
                    int ndx = input.IndexOf("=");
                    string lstr = input.Substring(0, ndx).Trim();
                    string rstr = input.Substring(ndx + 1).Trim();
                    if (rstr.CompareTo(lstr) == 0)
                    {
                        return true;
                    }
                    bool bIsBoolean;
                    Variable var = _varProvider[lstr.Trim()];

                    if (rstr.IndexOf("\"") > -1)
                    {
                        string strTrim = rstr.Trim();

                        string strSub = strTrim.Substring(1, strTrim.Length - 2);
                        if (var.StringValue.CompareTo(strSub) == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (Utilities.IsStringBoolean(out bIsBoolean, rstr))
                    {
                        if (var != null)
                        {
                            if (var.BoolValue == bIsBoolean)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            errors.Append("\n*********** WIZARD ERROR ************");
                            errors.Append("\nVariable: " + lstr + " does not exist");
                            errors.Append("\n*************************************");
                        }
                    }
                    else
                    {

                        Evaluate(lstr);
                        decimal lval = _resultAsDecimal;
                        if (_isStringCompare)
                        {
                            if (var.StringValue.CompareTo(rstr) == 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            Evaluate(rstr);
                            decimal rval = _resultAsDecimal;

                            if (lval == rval)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    Variable var = _varProvider[input.Trim()];
                    if (var != null)
                    {
                        if (var.VariableType == VariableTypes.BOOL)
                        {
                            return var.BoolValue;
                        }
                    }
                    else
                    {
                        bool bIsTrue;
                        if (Utilities.IsStringBoolean(out bIsTrue, input.Trim()))
                        {
                            return bIsTrue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                errors.Append("\n" + input + " Has Errors !!!");
                errors.Append("Error in Compare: " + e.Message + e.StackTrace);
                return false;
            }
        }

        private bool HasOperator(string input)
        {
            bool bHasOperator = false;
            try
            {
                if (input.IndexOf("|") > 0
                    || input.IndexOf("&") > 0
                    || input.IndexOf("*") > 0
                    || input.IndexOf("/") > 0
                    || input.IndexOf("-") > 0
                    || input.IndexOf("+") > 0
                    || input.IndexOf("=") > 0
                    || input.IndexOf(">") > 0
                    || input.IndexOf("<") > 0
                    || input.IndexOf("(") > 0
                    || input.IndexOf(")") > 0)
                {
                    bHasOperator = true;
                }
                else
                {
                    bHasOperator = false;
                }
            }
            catch
            {
                return false;
            }
            return bHasOperator;
        }

        private bool IsCompoundComparison(string expr)
        {
            try
            {
                if (expr.IndexOf("|") > 0 || expr.IndexOf("&") > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        private bool ProcessComparison(string expr)
        {
            try
            {
                bool rb = false;

                int iEq = expr.IndexOf("=");

                char[] ors = { '|' };
                char[] ands = { '&' };

                string[] orStrs = expr.Split(ors);
                string[] andStrs = expr.Split(ands);

                if (orStrs.Length > 1)
                {
                    for (int o = 0; o < orStrs.Length; o++)
                    {
                        string strOrCheck = orStrs[o];
                        rb = Compare(strOrCheck);
                        if (!rb)
                        {
                            break;
                        }
                    }
                }

                if (andStrs.Length > 1)
                {
                    for (int a = 0; a < andStrs.Length; a++)
                    {
                        string strAndCheck = andStrs[a];
                        rb = Compare(strAndCheck);
                        if (!rb)
                        {
                            break;
                        }
                    }
                }

                return rb;
            }
            catch
            {
                return false;
            }
        }
        private int AndOrLocate(string expr, int p)
        {
            try
            {
                char[] d = { '|', '&' };
                return expr.IndexOfAny(d, p);
            }
            catch
            {
                return p;
            }
        }
        private bool IsComparison(string expr)
        {
            try
            {

                if (expr.IndexOf("<") > 0)
                {
                    return true;
                }
                else if (expr.IndexOf(">") > 0)
                {
                    return true;
                }
                else if (expr.IndexOf("=") > 0)
                {
                    return true;
                }
                else if (expr.IndexOf(" isa ") > 0)
                {
                    return true;
                }
                else if (expr.IndexOf(" hasa ") > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void Assignment(ref decimal r)
        {
            try
            {
                AddOrSubtract(ref r);
            }
            catch (Exception e)
            {
                errors.Append("\nError in assignment: " + e.Message + "\n" + e.StackTrace);
            }
        }

        private void AddOrSubtract(ref decimal r)
        {
            try
            {
                char o;
                decimal d = 0;

                MultiplyDivideOrMod(ref r);
                while ((o = _token[0]) == '+' || o == '-')
                {
                    Parse();
                    MultiplyDivideOrMod(ref d);

                    if (o == '+')
                        r = r + d;
                    else if (o == '-')
                        r = r - d;
                }
            }
            catch (Exception e)
            {
                errors.Append("\nError in addOrSubtract: " + e.Message + "\n" + e.StackTrace);
            }
        }

        private void MultiplyDivideOrMod(ref decimal r)
        {
            try
            {
                char o;
                decimal d = 0;

                PowerUp(ref r);
                while ((o = _token[0]) == '*' || o == '/' || o == '%')
                {
                    Parse();
                    PowerUp(ref d);
                    if (o == '*')
                        r = r * d;
                    else if (o == '/')
                    {
                        r = r / d;
                    }
                    else if (o == '%')
                    {
                        r = r % d;
                    }
                }
            }
            catch (Exception e)
            {
                errors.Append("\nError in multiDivOrMod: " + e.Message + "\n" + e.StackTrace);
            }
        }

        private void PowerUp(ref decimal r)
        {
            try
            {
                decimal d = 0;

                PlusOrMinus(ref r);
                if (_token[0] == '^')
                {
                    Parse();
                    PlusOrMinus(ref d);
                    double dr = Convert.ToDouble(r);
                    double dd = Convert.ToDouble(d);
                    r = Convert.ToDecimal(Math.Pow(dr, dd));
                }
            }
            catch (Exception e)
            {
                errors.Append("\nError in powerUp: " + e.Message + "\n" + e.StackTrace);
            }
        }

        private void PlusOrMinus(ref decimal r)
        {
            try
            {
                char o = '0';

                if (_token[0] == '+' || _token[0] == '-')
                {
                    o = _token[0];
                    Parse();
                }
                Literal(ref r);
                if (o == '-')
                    r = -r;
            }
            catch (Exception e)
            {
                errors.Append("\nError in plusOrMinus: " + e.Message + "\n" + e.StackTrace);
            }
        }

        private void Literal(ref decimal r)
        {
            try
            {
                if (_token[0] == '(')
                {
                    Parse();
                    Assignment(ref r);
                    Parse();
                }
                else
                {
                    if (_type == NUM)
                    {
                        string p = new String(_token);
                        r = decimal.Parse(p);
                        Parse();
                    }
                    else if (_type == VAR)
                    {
                        string var = new String(_token);

                        switch (var)
                        {
                            case "sin":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Sin(ds));
                                    Parse();
                                    break;
                                }
                            case "cos":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Cos(ds));
                                    Parse();
                                    break;
                                }
                            case "tan":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Tan(ds));
                                    Parse();
                                    break;
                                }
                            case "asin":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Asin(ds));
                                    Parse();
                                    break;
                                }
                            case "acos":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Acos(ds));
                                    Parse();
                                    break;
                                }
                            case "atan":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Atan(ds));
                                    Parse();
                                    break;
                                }
                            case "sinh":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Sinh(ds));
                                    Parse();
                                    break;
                                }
                            case "cosh":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Cosh(ds));
                                    Parse();
                                    break;
                                }
                            case "tanh":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Tanh(ds));
                                    Parse();
                                    break;
                                }
                            case "exp":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Exp(ds));
                                    Parse();
                                    break;
                                }
                            case "log":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Log(ds));
                                    Parse();
                                    break;
                                }
                            case "log10":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Log10(ds));
                                    Parse();
                                    break;
                                }
                            case "sqrt":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Sqrt(ds));
                                    Parse();
                                    break;
                                }
                            case "floor":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Floor(ds));
                                    Parse();
                                    break;
                                }
                            case "ceil":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Ceiling(ds));
                                    Parse();
                                    break;
                                }
                            case "abs":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = Convert.ToDecimal(Math.Abs(ds));
                                    Parse();
                                    break;
                                }

                            case "deg":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = ToDegrees(ds);
                                    Parse();
                                    break;
                                }
                            case "rad":
                                {
                                    Parse();
                                    Parse();
                                    decimal s = 0;
                                    Assignment(ref s);
                                    double ds = Convert.ToDouble(s);
                                    r = ToRadians(ds);
                                    Parse();
                                    break;
                                }
                            case "min":
                                {
                                    Parse();
                                    Parse();
                                    decimal s1 = 0;
                                    Assignment(ref s1);
                                    Parse();
                                    decimal s2 = 0;
                                    Assignment(ref s2);
                                    Parse();
                                    r = Math.Min(s1, s2);
                                    break;
                                }
                            case "max":
                                {
                                    Parse();
                                    Parse();
                                    decimal s1 = 0;
                                    Assignment(ref s1);
                                    Parse();
                                    decimal s2 = 0;
                                    Assignment(ref s2);
                                    Parse();
                                    r = Math.Max(s1, s2);
                                    break;
                                }
                            case "round":
                                {
                                    Parse();
                                    Parse();
                                    decimal s1 = 0;
                                    Assignment(ref s1);
                                    Parse();
                                    decimal s2 = 0;
                                    Assignment(ref s2);
                                    Parse();
                                    int d2 = Convert.ToInt32(Math.Round(s2));
                                    r = Math.Round(s1, d2);
                                    break;
                                }
                            default:
                                {
                                    string v = new String(_token);
                                    r = _varProvider[v].DecimalValue;
                                    break;
                                }
                        }
                        Parse();
                        return;
                    }
                    else
                        return;
                }
            }
            catch (Exception e)
            {
                errors.Append("\nError in literal: " + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region Parsing
        private void Parse()
        {
            try
            {
                _type = NONE;
                if (_pos < _expr.Length)
                {
                    StringBuilder sb = new StringBuilder();
                    if (_expr[_pos] == '~')
                        return;

                    while (IsWhite(_expr[_pos]))
                        _pos++;

                    if (IsDelimiter(_expr[_pos]))
                    {
                        if (_noOperator && IsMinus(_expr[_pos]))
                        {
                            _type = NUM;
                        }
                        else
                        {
                            _type = DEL;
                        }
                        sb.Append(_expr[_pos]);
                        _pos++;
                    }
                    else if (IsNumeric(_expr[_pos]))
                    {
                        _type = NUM;
                        while (IsNumeric(_expr[_pos]))
                        {
                            sb.Append(_expr[_pos]);
                            _pos++;
                            if (_pos == _expr.Length)
                            {
                                break;
                            }
                        }
                    }
                    else if (IsAlpha(_expr[_pos]))
                    {
                        _type = VAR;
                        while (IsAlpha(_expr[_pos]))
                        {
                            if(IsDelimiter(_expr[_pos]))
                            {
                                break;
                            }
                            else if (_pos < _expr.Length - 1)
                            {
                                sb.Append(_expr[_pos]);
                                _pos++;
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                    _token = sb.ToString().ToCharArray();
                }
            }
            catch (Exception e)
            {
                _token = new Char[] { '0' };
                errors.Append("\nError in Parse: " + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion
    }
}
