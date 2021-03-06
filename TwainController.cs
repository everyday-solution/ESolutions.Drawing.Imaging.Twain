using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ESolutions.Drawing.Imaging.Twain
{
	/// <summary>
	/// This class can be used to select a twain data source and acquire data from it. Mention that a call to the
	/// BeginAcquire method starts the acquiring process asynchroniously. Hook to the ScannedAll and/or the ScannedOne
	/// event to receive image data acquired from data sources. Prevent calling BeginAcquire repeatedly and different 
	/// instances of this class. Doing so can lead to undefinied circumstances.
	/// </summary>
	public class TwainController
	{
		#region BeginAcquire
		/// <summary>
		/// Begins aquiring data from the current twain data source. A call to this method
		/// begins a asynchronous scanning process with a preview window. Rember invoking
		/// events fired by objects of this class when using forms.
		/// </summary>
		public void BeginAcquire ()
		{
			Thread acquiringThread = new Thread (new ThreadStart (AcquireThreadStart));
			acquiringThread.Start ();
		}
		#endregion

		#region Select
		/// <summary>
		/// Shows up a dialog from which the user can choose a twain data source
		/// </summary>
		public void Select ()
		{
			TwainPreviewForm hidden = new TwainPreviewForm ();
			hidden.Controller.Select ();
		}
		#endregion

		#region ScannedAll
		/// <summary>
		/// This event is fired after the last page on the scanner has been scanned.
		/// </summary>
		public event ScannedAllEventHandler ScannedAll;
		#endregion

		#region ScannedOne
		/// <summary>
		/// This event is fired each time a whole page has been acquired from the data source.
		/// </summary>
		public event ScannedOneEventHandler ScannedOne;
		#endregion

		#region AcquireThreadStart
		/// <summary>
		/// This method is invoked in BeginAcquire. Here the scanning process begins.
		/// </summary>
		private void AcquireThreadStart ()
		{
			TwainPreviewForm hidden = new TwainPreviewForm ();
			hidden.Controller.ScannedAll += new ScannedAllEventHandler (InternalController_ScannedAll);
			hidden.Controller.ScannedOne += new ScannedOneEventHandler (InternalController_ScannedOne);
			Application.Run (hidden);
		}
		#endregion

		#region InternalController_ScannedAll
		/// <summary>
		/// This event handler listens to the internal twain controller that is co working with
		/// this preoview from and handles the communication with the twain interface. It is just used
		/// to lead through the event from the internal controller.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InternalController_ScannedAll (object sender, ScannedAllEventArgs e)
		{
			if (this.ScannedAll != null)
			{
				this.ScannedAll (sender, e);
			}
		}
		#endregion

		#region InternalController_ScannedOne
		/// This event handler listens to the internal twain controller that is co working with
		/// this preoview from and handles the communication with the twain interface. It is just used
		/// to lead through the event from the internal controller.
		private void InternalController_ScannedOne (object sender, ScannedOneEventArgs e)
		{
			if (this.ScannedOne != null)
			{
				this.ScannedOne (sender, e);
			}
		}
		#endregion
	}
}
