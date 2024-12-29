using BusinessDVLD;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.GloabalClasses
{
    public static class clsGloabal
    {
        public static clsUser CurrentUser;

        public static bool RememberUserNameAndPassword(string UserName, string Password)
        {
            try
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\DVLD", "UserName", UserName);
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\DVLD", "Password", Password);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        public static bool GetCurrentCredentials(ref string UserName, ref string Password)
        {
            try
            {
                UserName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\DVLD", "UserName", "Does not exist");
                Password = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\DVLD", "Password", "Does not exist");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
