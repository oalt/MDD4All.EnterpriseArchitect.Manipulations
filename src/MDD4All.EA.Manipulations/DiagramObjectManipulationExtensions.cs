using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
#if FACADE
using EAAPI = MDD4All.EAFacade;
#else
using EAAPI = EA;
#endif

#if EAAPI
namespace MDD4All.EAFacade.Manipulations
#else
namespace MDD4All.EnterpriseArchitect.Manipulations
#endif
{
	public static class DiagramObjectManipulationExtensions
	{
		public static Point GetLabelSize(this EAAPI.DiagramObject portDiagramObject)
		{
			Point result = new Point();

			string style = portDiagramObject.Style.ToString();

			char[] semicolonSperator = { ';' };

			string[] styleTokens = style.Split(semicolonSperator);

			string labelToken = styleTokens.FirstOrDefault(token => token.StartsWith("LBL="));

			if(!string.IsNullOrEmpty(labelToken))
			{
				labelToken = labelToken.Replace("LBL=", "");

				char[] colonSeparator = { ':' };

				string[] labelPropertyTokens = labelToken.Split(colonSeparator);

				string cx = labelPropertyTokens.FirstOrDefault(cxToken => cxToken.StartsWith("CX="))?.Replace("CX=", "");

				string cy = labelPropertyTokens.FirstOrDefault(cxToken => cxToken.StartsWith("CY="))?.Replace("CY=", "");

				int resultX = 0;

				int.TryParse(cx, out resultX);

				int resultY = 0;

				int.TryParse(cy, out resultY);

				result.X = resultX;
				result.Y = resultY;
			}

			return result;
		}

		public static Point GetLabelOffset(this EAAPI.DiagramObject portDiagramObject)
		{
			Point result = new Point();

			string style = portDiagramObject.Style.ToString();

			char[] semicolonSperator = { ';' };

			string[] styleTokens = style.Split(semicolonSperator);

			string labelToken = styleTokens.FirstOrDefault(token => token.StartsWith("LBL="));

			if (!string.IsNullOrEmpty(labelToken))
			{
				labelToken = labelToken.Replace("LBL=", "");

				char[] colonSeparator = { ':' };

				string[] labelPropertyTokens = labelToken.Split(colonSeparator);

				string cx = labelPropertyTokens.FirstOrDefault(cxToken => cxToken.StartsWith("OX="))?.Replace("OX=", "");

				string cy = labelPropertyTokens.FirstOrDefault(cxToken => cxToken.StartsWith("OY="))?.Replace("OY=", "");

				int resultX = 0;

				int.TryParse(cx, out resultX);

				int resultY = 0;

				int.TryParse(cy, out resultY);

				result.X = resultX;
				result.Y = resultY;
			}

			return result;
		}
	}
}
