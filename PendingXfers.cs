using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ESolutions.Drawing.Imaging.Twain
{
	/// <summary>
	/// Structure storing information about pending xfers
	/// </summary>
	[StructLayout (LayoutKind.Sequential, Pack = 2)]
	internal class PendingXfersInterop
	{
		#region Count
		/// <summary>
		/// Amount of pending xfers
		/// </summary>
		public short Count; 
		#endregion

		#region EOJ
		/// <summary>
		/// 
		/// </summary>
		public int EOJ; 
		#endregion
	}
}
