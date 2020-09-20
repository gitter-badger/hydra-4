﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using Microsoft.VisualStudio.TextTemplating;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

namespace Utils
{
    public class TemplateEngineHost
    {
        private static bool bSkipErrors;
        public event EventHandler OnDebugCallback;

        public TemplateEngineHost()
        {
        }

        private void DebugCallback(object sender, EventArgs e)
        {
            OnDebugCallback(sender, e);
        }

        public string Generate<T>(Dictionary<string, object> sessionVariables, bool throwException = false)
        {
            try
            {
                var generatorType = typeof(T);
                var generator = Activator.CreateInstance(generatorType);
                var session = new TextTemplatingSession();

                session["DebugCallback"] = new EventHandler(DebugCallback);

                foreach (var pair in sessionVariables)
                {
                    session[pair.Key] = pair.Value;
                }

                generatorType.GetProperty("Session").SetValue(generator, session, null);
                generatorType.GetMethod("Initialize").Invoke(generator, null);

                var output = (string)generatorType.GetMethod("TransformText").Invoke(generator, null);

                return output;
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    throw ex;
                }
                else if (MessageBox.Show(string.Format("Generator threw an error '{0}'. Would you like to debug?", ex.Message), "Generator error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bSkipErrors = false;
                    Debugger.Break();
                }
                else
                {
                    bSkipErrors = true;
                }
            }

            return null;
        }
    }
}