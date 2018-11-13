using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    public class Utilities
    {
        static char[] numberChars = { '-', '+', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        /// <summary>
        /// For debug timing tests
        /// </summary>
        public static DateTime d1;

        /// <summary>
        /// Useless constructor.
        /// </summary>
        public Utilities()
        {
        }

        #region Relative path munging
        /// <summary>
        /// Outputs the relative path supplied as an absolute path using the basepath supplied.
        /// </summary>
        /// <param name="sPath">The path to output</param>
        /// <param name="sBasepath">the base path to compare to</param>
        /// <param name="sRelpath">the relative path</param>
        public static void RelativeToAbsolutePath(out string sPath, string sBasepath, string sRelpath)
        {
            // determine relative child to parent distance
            StringBuilder sb = new StringBuilder();
            char[] chars = sRelpath.ToCharArray();

            bool bADot = false;
            int iUpDirs = 0;

            int iStartNdx = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                if (c == '.')
                {
                    if (bADot)
                    {
                        iUpDirs++;
                    }
                    bADot = true;
                }
                else if (c == '\\')
                {
                    bADot = false;
                }
                else
                {
                    iStartNdx = i;
                    bADot = false;
                    break;
                }
            }

            if (iUpDirs > 0)
            {
                string sDir = sBasepath;
                for (int d = 0; d < iUpDirs; d++)
                {
                    DirectoryInfo di = Directory.GetParent(sDir);
                    sDir = di.FullName;
                }
            }

            sPath = sBasepath + sRelpath.Substring(iStartNdx - 1);
        }
        /// <summary>
        /// Ouputs the relative path from the absolute path supplied
        /// </summary>
        /// <param name="sPath">the output path</param>
        /// <param name="sAbsPath">the absolute path</param>
        /// <param name="sBasepath">the base path to compare to</param>
        /// <returns>true is successful</returns>
        public static bool AbsPathMunge(out string sPath, string sAbsPath, string sBasepath)
        {
            string strAbs = sAbsPath.ToLower();
            string strBase = sBasepath.ToLower();

            if (strAbs.StartsWith(strBase))
            {
                string str = "." + sAbsPath.Substring(sBasepath.Length);
                sPath = str;
                return true;
            }
            else
            {
                sPath = sAbsPath;
                return false;
            }
        }
        #endregion

        #region Date Munging
        /// <summary>
        /// Ouputs a DateTime object from the string supplied.
        /// </summary>
        /// <param name="oDateTime">the DateTime object ouput</param>
        /// <param name="sDate">the string of the date</param>
        /// <returns>true if successful</returns>
        public static bool MakeDateFromString(out DateTime oDateTime, string sDate)
        {
            try
            {
                System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex("^{n}/{n}/{n:4}");
                if (regEx.IsMatch(sDate))
                {
                    oDateTime = DateTime.Parse(sDate);
                    return true;
                }
                else
                {
                    oDateTime = DateTime.Now;
                    return false;
                }
            }
            catch
            {
                oDateTime = DateTime.Now;
                return false;
            }
        }
        #endregion

        #region Number munging
        /// <summary>
        /// Outputs the integer equivalent of the string
        /// </summary>
        /// <param name="strI">the Integer string</param>
        /// <param name="iNumber">the int output</param>
        /// <returns>true if successful</returns>
        public static bool StringToInteger(string strI, out int iNumber)
        {
            try
            {
                if (IsANumber(strI))
                {
                    iNumber = int.Parse(strI.Trim());
                    return true;
                }
                else
                {
                    iNumber = 0;
                    return false;
                }
            }
            catch
            {
                iNumber = 0;
                return false;
            }
        }

        /// <summary>
        /// Outputs the double value of the string supplied
        /// </summary>
        /// <param name="str">the string of the double</param>
        /// <param name="dNumber">the double ouput</param>
        /// <returns>true if successful</returns>
        public static bool StringToDouble(string str, out double dNumber)
        {
            try
            {
                if (IsANumber(str))
                {
                    dNumber = double.Parse(str.Trim());
                    return true;
                }
                else
                {
                    dNumber = 0;
                    return false;
                }
            }
            catch
            {
                dNumber = 0;
                return false;
            }
        }

        /// <summary>
        /// Outputs the double value of the string supplied
        /// </summary>
        /// <param name="str">the string of the double</param>
        /// <param name="dNumber">the double ouput</param>
        /// <returns>true if successful</returns>
        public static bool StringToDecimal(string str, out decimal dNumber)
        {
            try
            {
                if (IsANumber(str))
                {
                    dNumber = decimal.Parse(str.Trim());
                    return true;
                }
                else
                {
                    dNumber = 0;
                    return false;
                }
            }
            catch
            {
                dNumber = 0;
                return false;
            }
        }
        /// <summary>
        /// Verifies that the string is composed of digits and signs exclusively.
        /// </summary>
        /// <param name="str">string to check</param>
        /// <returns>True if it is a number, false otherwise</returns>
        public static bool IsANumber(string str)
        {
            char[] chrs = str.ToCharArray();
            foreach (char c in chrs)
            {
                if (!IsADigit(c))
                {
                    return false;
                }

            }
            return true;
        }

        static bool IsADigit(char c)
        {
            bool isOne = false;
            foreach (char x in numberChars)
            {
                if (c.CompareTo(x) == 0)
                {
                    isOne = true;
                }
            }
            return isOne;
        }
        /// <summary>
        /// Determines if the string is a representation of a whole number greater than zero
        /// </summary>
        /// <param name="str">the string of the number</param>
        /// <returns>true if successful</returns>
        public static bool IsPositiveWholeNumber(string str)
        {
            bool bIsPos = false;

            char[] chars = str.Trim().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                if (c == '.' || c == '-')
                {
                    bIsPos = false;
                    break;
                }
                else if (!char.IsDigit(c))
                {
                    bIsPos = false;
                    break;
                }
                else
                {
                    bIsPos = true;
                }
            }

            return bIsPos;
        }
        #endregion

        #region Normalizing
        /// <summary>
        /// Converts characters reserved for XML syntax to equvalent values that can be used by XML.
        /// </summary>
        /// <example>
        /// Utilities.NormalizeXML("<SomeXmlNode expression="5&lt;6"></SomeXmlNode><br/>
        /// Outputs<br/>
        /// 5&lt;6<br/>
        /// </example>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NormalizeXML(string str)
        {
            try
            {
                string strRet;

                string lt = "<";
                string strLt = "&lt;";

                string gt = ">";
                string strGt = "&gt;";

                string amp = "&";
                string strAmp = "&amp;";

                strRet = str.Replace(amp, strAmp);
                string strOne = strRet.Replace(lt, strLt);
                string strTwo = strOne.Replace(gt, strGt);

                return strTwo;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Checks for characters reserved for XML Syntax and encloses the string in a CDATA section.
        /// </summary>
        /// <param name="sString"></param>
        /// <returns></returns>
        public static string MakeAppropriateCDATA(string sString)
        {
            string sRet = sString;
            char[] checks = { '<', '>', '&', '\"' };
            if (sString.IndexOfAny(checks, 0) > -1)
            {
                sRet = "<![CDATA[" + sString + "]]>";
            }
            return sRet;
        }
        #endregion

        #region Directory/File copying
        /// <summary>
        /// Copies file and folders from the source location to the destination location.
        /// </summary>
        /// <param name="src">the source location</param>
        /// <param name="dest">the destination location</param>
        public static void CopyLocation(string src, string dest)
        {
            try
            {
                string[] srcDirs = Directory.GetDirectories(src);
                string[] srcFiles = Directory.GetFiles(src);

                if (!Directory.Exists(dest))
                {
                    Directory.CreateDirectory(dest);
                }

                for (int f = 0; f < srcFiles.Length; f++)
                {
                    string srcFilename = srcFiles[f];
                    FileInfo fi = new FileInfo(srcFilename);
                    string destFilename = dest + "\\" + fi.Name;
                    File.Copy(srcFilename, destFilename, true);
                }

                for (int d = 0; d < srcDirs.Length; d++)
                {
                    string sDir = srcDirs[d];
                    DirectoryInfo di = new DirectoryInfo(sDir);
                    string dirParent = di.Parent.FullName;
                    int parentLen = dirParent.Length;
                    string newDestDir = dest + sDir.Substring(parentLen);

                    CopyLocation(sDir, newDestDir);
                }

            }
            catch
            {
            }
        }
        /// <summary>
        /// Copies the directory contents from the source directory to the destination directory.
        /// </summary>
        /// <param name="srcDir">the source directory</param>
        /// <param name="destDir">the destination directory</param>
        public static void CopyDirectory(string srcDir, string destDir)
        {
            string[] srcDirs = Directory.GetDirectories(srcDir);
            string[] srcFiles = Directory.GetFiles(srcDir);

            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            for (int f = 0; f < srcFiles.Length; f++)
            {
                string srcFilename = srcFiles[f];
                FileInfo fi = new FileInfo(srcFilename);
                string destFilename = destDir + "\\" + fi.Name;
                File.Copy(srcFilename, destFilename, true);
            }

            for (int d = 0; d < srcDirs.Length; d++)
            {
                string sDir = srcDirs[d];
                DirectoryInfo di = new DirectoryInfo(sDir);
                string dirParent = di.Parent.FullName;
                int parentLen = dirParent.Length;
                string newDestDir = destDir + sDir.Substring(parentLen);

                CopyDirectory(sDir, newDestDir);
            }
        }
        /// <summary>
        /// Copies all subdirectories from the source directory to the destination directory
        /// </summary>
        /// <param name="srcParentDir">the "Parent" directory</param>
        /// <param name="destDir">the destination directory</param>
        public static void CopySubDirectories(string srcParentDir, string destDir)
        {
            string[] srcDirs = Directory.GetDirectories(srcParentDir);

            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            for (int d = 0; d < srcDirs.Length; d++)
            {
                string sDir = srcDirs[d];
                DirectoryInfo di = new DirectoryInfo(sDir);
                string dirParent = di.Parent.FullName;
                int parentLen = dirParent.Length;
                string newDestDir = destDir + sDir.Substring(parentLen);

                CopyDirectory(sDir, newDestDir);
            }
        }
        #endregion

        #region Text Recognition (True false knowing)
        /// <summary>
        /// Determines if the supplied string corresponds to a word that means true or false.
        /// </summary>
        /// <param name="bIsTrue">Ouput of boolean resolved from the string</param>
        /// <param name="str">string to examine</param>
        /// <returns>true if successful and false otherwise</returns>
        public static bool IsStringBoolean(out bool bIsTrue, string str)
        {
            string strLower = str.Trim().ToLower();
            if (strLower.CompareTo("true") == 0)
            {
                bIsTrue = true;
                return true;
            }
            else if (strLower.CompareTo("false") == 0)
            {
                bIsTrue = false;
                return true;
            }
            else
            {
                bIsTrue = false;
                return false;
            }
        }
        /// <summary>
        /// Determines if the supplied string represents a True boolean
        /// </summary>
        /// <param name="str">the string of the boolean</param>
        /// <returns>true if the string is correct for true and false otherwise</returns>
        public static bool IsStringTrue(string str)
        {
            string strLower = str.Trim().ToLower();
            if (strLower.CompareTo("true") == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Debug
        /// <summary>
        /// Sets a starting TimeDate object.              
        /// </summary>
        public static void SetTimeStart()
        {
            d1 = DateTime.Now;
        }
        /// <summary>
        /// returns the elasped time since the SetTimeStart() has been called.
        /// </summary>
        /// <returns>the elasped time in milliseconds</returns>
        public static string GetElaspedTime()
        {
            DateTime d2 = DateTime.Now;
            TimeSpan dElasped = d2 - d1;

            return "" + dElasped.TotalMilliseconds;
        }
        #endregion

    }
}
