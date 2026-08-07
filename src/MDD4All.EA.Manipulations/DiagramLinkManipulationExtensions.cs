using System;
using System.Collections.Generic;
using System.Drawing;

#if EA_FACADE
using EAAPI = MDD4All.EAFacade.DataModels.Contracts;
#else
using EAAPI = EA;
#endif

#if EA_FACADE
namespace MDD4All.EAFacade.Manipulations
#else
namespace MDD4All.EnterpriseArchitect.Manipulations
#endif
{
    public static class DiagramLinkManipulationExtensions
    {
        public static string GetGeometryValue(this EAAPI.DiagramLink diagramLink, string key)
        {
            string result;
            List<KeyValuePair<string, string>> tokens = ParseKeyValueString(diagramLink.Geometry);
            int existingIndex = tokens.FindIndex(currentToken => currentToken.Key == key);
            if (existingIndex >= 0)
            {
                result = tokens[existingIndex].Value;
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static void SetGeometryValue(this EAAPI.DiagramLink diagramLink, string key, string value)
        {
            List<KeyValuePair<string, string>> tokens = ParseKeyValueString(diagramLink.Geometry);
            SetOrAddValue(tokens, key, value);
            diagramLink.Geometry = BuildKeyValueString(tokens);
            diagramLink.Update();
        }

        public static string GetStyleValue(this EAAPI.DiagramLink diagramLink, string key)
        {
            string result;
            List<KeyValuePair<string, string>> tokens = ParseKeyValueString(diagramLink.Style);
            int existingIndex = tokens.FindIndex(currentToken => currentToken.Key == key);
            if (existingIndex >= 0)
            {
                result = tokens[existingIndex].Value;
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static void SetStyleValue(this EAAPI.DiagramLink diagramLink, string key, string value)
        {
            List<KeyValuePair<string, string>> tokens = ParseKeyValueString(diagramLink.Style);
            SetOrAddValue(tokens, key, value);
            diagramLink.Style = BuildKeyValueString(tokens);
            diagramLink.Update();
        }

        public static List<Point> GetPathPoints(this EAAPI.DiagramLink diagramLink)
        {
            List<Point> result = new List<Point>();
            char[] semicolonSeparator = { ';' };
            char[] colonSeparator = { ':' };
            string[] pointTokens = diagramLink.Path.Split(semicolonSeparator, StringSplitOptions.RemoveEmptyEntries);

            foreach (string currentPointToken in pointTokens)
            {
                string[] coordinateTokens = currentPointToken.Split(colonSeparator);
                int pointX = 0;
                int pointY = 0;
                if (coordinateTokens.Length == 2)
                {
                    int.TryParse(coordinateTokens[0], out pointX);
                    int.TryParse(coordinateTokens[1], out pointY);
                }
                result.Add(new Point(pointX, pointY));
            }
            return result;
        }

        public static void SetPathPoints(this EAAPI.DiagramLink diagramLink, IEnumerable<Point> points)
        {
            string result = "";
            foreach (Point currentPoint in points)
            {
                result += currentPoint.X + ":" + currentPoint.Y + ";";
            }
            diagramLink.Path = result;
            diagramLink.Update();
        }

        // Splits on ';', then on the FIRST '=' within a token, so nested values that contain
        // their own '=' (e.g. "LLT=CX=86:CY=14:...") stay intact as a single opaque value.
        private static List<KeyValuePair<string, string>> ParseKeyValueString(string source)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            char[] semicolonSeparator = { ';' };
            string[] tokens = source.Split(semicolonSeparator, StringSplitOptions.RemoveEmptyEntries);

            foreach (string currentToken in tokens)
            {
                int equalsIndex = currentToken.IndexOf('=');
                string key;
                string value;
                if (equalsIndex >= 0)
                {
                    key = currentToken.Substring(0, equalsIndex);
                    value = currentToken.Substring(equalsIndex + 1);
                }
                else
                {
                    key = currentToken;
                    value = "";
                }
                result.Add(new KeyValuePair<string, string>(key, value));
            }
            return result;
        }

        private static string BuildKeyValueString(List<KeyValuePair<string, string>> tokens)
        {
            string result = "";
            foreach (KeyValuePair<string, string> currentToken in tokens)
            {
                result += currentToken.Key + "=" + currentToken.Value + ";";
            }
            return result;
        }

        // Replaces an existing key in place (preserving its position) or appends a new one at
        // the end, so every other key keeps both its value and its original order.
        private static void SetOrAddValue(List<KeyValuePair<string, string>> tokens, string key, string value)
        {
            int existingIndex = tokens.FindIndex(currentToken => currentToken.Key == key);
            if (existingIndex >= 0)
            {
                tokens[existingIndex] = new KeyValuePair<string, string>(key, value);
            }
            else
            {
                tokens.Add(new KeyValuePair<string, string>(key, value));
            }
        }
    }
}
