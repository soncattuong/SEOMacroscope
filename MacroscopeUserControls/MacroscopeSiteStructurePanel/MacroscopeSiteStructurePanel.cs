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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeSiteStructurePanel.
  /// </summary>

  public partial class MacroscopeSiteStructurePanel : MacroscopeUserControl
  {

    /**************************************************************************/

    private MacroscopeColumnSorter lvColumnSorter;
		    
    /**************************************************************************/
		    
    public MacroscopeSiteStructurePanel ()
    {
      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.lvColumnSorter = new MacroscopeColumnSorter ();
            
      this.tabControlSiteStructure.Dock = DockStyle.Fill;

      this.splitContainerSiteOverview.Dock = DockStyle.Fill;
      this.treeViewSiteOverview.Dock = DockStyle.Fill;

      this.tabControlKeywordAnalysisPhrases.Dock = DockStyle.Fill;
        
      this.listViewKeywordAnalysis1.Dock = DockStyle.Fill;
      this.listViewKeywordAnalysis2.Dock = DockStyle.Fill;
      this.listViewKeywordAnalysis3.Dock = DockStyle.Fill;
      this.listViewKeywordAnalysis4.Dock = DockStyle.Fill;

      this.listViewKeywordAnalysis1.ListViewItemSorter = this.lvColumnSorter;
      this.listViewKeywordAnalysis2.ListViewItemSorter = this.lvColumnSorter;
      this.listViewKeywordAnalysis3.ListViewItemSorter = this.lvColumnSorter;
      this.listViewKeywordAnalysis4.ListViewItemSorter = this.lvColumnSorter;

      this.listViewKeywordAnalysis1.ColumnClick += this.CallbackColumnClick;
      this.listViewKeywordAnalysis2.ColumnClick += this.CallbackColumnClick;
      this.listViewKeywordAnalysis3.ColumnClick += this.CallbackColumnClick;
      this.listViewKeywordAnalysis4.ColumnClick += this.CallbackColumnClick;

    }

    /**************************************************************************/

    void CallbackColumnClick ( object sender, ColumnClickEventArgs e )
    {

      ListView lvListView = sender as ListView;

      if( e.Column == lvColumnSorter.SortColumn )
      {
        if( lvColumnSorter.Order == SortOrder.Ascending )
        {
          lvColumnSorter.Order = SortOrder.Descending;
        }
        else
        {
          lvColumnSorter.Order = SortOrder.Ascending;
        }
      }
      else
      {
        lvColumnSorter.SortColumn = e.Column;
        lvColumnSorter.Order = SortOrder.Ascending;
      }

      lvListView.Sort();

    }

    /**************************************************************************/
    
  }

}
