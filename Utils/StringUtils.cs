using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Utils {
	/// <summary>
	/// Helper methods for manipulating string values
	/// </summary>
	public static class StringUtils {
		/// <summary>
		/// Stribs the file path and extension (if they exist) off of a file name
		/// </summary>
		/// <param name="fileName">filename to alter</param>
		/// <returns>filename with the path and extension ripped off if they were present</returns>
		public static string scrubPathAndExtFromFileName(string fileName) {
			fileName = scrubPathFromFileName(fileName);
			fileName = scrubExtensionFromFilename(fileName);
			
			return fileName;
		}

		/// <summary>
		/// Scrubs the directory information off of a file name
		/// </summary>
		/// <param name="fileName">filename to alter</param>
		/// <returns>filename with the path ripped off if it were present</returns>
		public static string scrubPathFromFileName(string fileName) {
			// do we need to scrub the directory information off?
			if (fileName.Contains("\\")) {
				fileName = fileName.Remove(0, fileName.LastIndexOf(@"\") + 1);
			}
			return fileName;
		}
		
		/// <summary>
		/// Scrubs the file extension from a file name
		/// </summary>
		/// <param name="fileName">filename to alter</param>
		/// <returns>filename with the extension ripped off if it were present</returns>
		public static string scrubExtensionFromFilename(string fileName) {
			// do we need to scrub the file extension off
			if (fileName.Contains(".")) {
				fileName = fileName.Remove(fileName.IndexOf("."));
			}
			return fileName;
		}
	}
}
