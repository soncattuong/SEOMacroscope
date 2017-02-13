﻿/*

	This file is part of SEOMacroscope.

	Copyright 2017 Jason Holland.

	The GitHub repository may be found at:

		https://github.com/nazuke/SEOMacroscope

	Foobar is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Foobar is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Foobar.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public class MacroscopeDisplayUriAnalysis : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayUriAnalysis ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
      : base( MainFormNew, lvListViewNew )
    {

      MainForm = MainFormNew;
      lvListView = lvListViewNew;

      if( MainForm.InvokeRequired )
      {
        MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              ConfigureListView();
            }
          )
        );
      }
      else
      {
        ConfigureListView();
      }

    }

    /**************************************************************************/

    protected override void ConfigureListView ()
    {
      if( !this.ListViewConfigured )
      {
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
    {

      string sStatus = msDoc.GetStatusCode().ToString();
      string sChecksum = msDoc.GetChecksum();
      string sCount = this.MainForm.GetJobMaster().GetDocCollection().GetStatsChecksumCount( Checksum: sChecksum ).ToString();
      string sPairKey = string.Join( "", sUrl );
      ListViewItem lvItem = null;

      this.lvListView.BeginUpdate();

      if( this.lvListView.Items.ContainsKey( sPairKey ) )
      {

        try
        {

          lvItem = this.lvListView.Items[ sPairKey ];
          lvItem.SubItems[ 0 ].Text = sUrl;
          lvItem.SubItems[ 1 ].Text = sStatus;
          lvItem.SubItems[ 2 ].Text = sCount;
          lvItem.SubItems[ 3 ].Text = sChecksum;

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplayUriAnalysis 1: {0}", ex.Message ) );
        }

      }
      else
      {

        try
        {

          lvItem = new ListViewItem ( sPairKey );
          lvItem.UseItemStyleForSubItems = false;
          lvItem.Name = sPairKey;

          lvItem.SubItems[ 0 ].Text = sUrl;
          lvItem.SubItems.Add( sStatus );
          lvItem.SubItems.Add( sCount );
          lvItem.SubItems.Add( sChecksum );

          this.lvListView.Items.Add( lvItem );

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplayUriAnalysis 2: {0}", ex.Message ) );
        }

      }

      if( lvItem != null )
      {

        lvItem.UseItemStyleForSubItems = false;
        lvItem.ForeColor = Color.Blue;

        if( Regex.IsMatch( sStatus, "^[2]" ) )
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Green;
        }
        else
        if( Regex.IsMatch( sStatus, "^[3]" ) )
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Goldenrod;
        }
        else
        if( Regex.IsMatch( sStatus, "^[45]" ) )
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Red;
        }
        else
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Blue;
        }

      }

      this.lvListView.EndUpdate();

    }

    /**************************************************************************/

  }

}
