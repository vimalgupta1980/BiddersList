using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SysconCommon.Algebras.DataTables;

namespace BiddersList
{
    /// <summary>
    /// 
    /// </summary>
    public class SysconBidderListDataModel
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public SysconBidderListDataModel()
        {

        }

        #region Properties

        [ColumnOrder(10)]
        public int RecNum
        {
            get;
            set;
        }

        [ColumnOrder(20)]
        public int Id
        {
            get;
            set;
        }

        [ColumnOrder(30)]
        public decimal VndTyp//VendorType
        {
            get;
            set;
        }

        [ColumnOrder(40)]
        public string VndNme//VendorName
        {
            get;
            set;
        }

        [ColumnOrder(50)]
        public string Contct//Contact
        {
            get;
            set;
        }

        [ColumnOrder(60)]
        public string Addrs1//Address1
        {
            get;
            set;
        }

        [ColumnOrder(70)]
        public string Addrs2//Address2
        {
            get;
            set;
        }

        [ColumnOrder(80)]
        public string CtyNme//City
        {
            get;
            set;
        }

        [ColumnOrder(90)]
        public string State_
        {
            get;
            set;
        }

        [ColumnOrder(100)]
        public string ZipCde//ZipCode
        {
            get;
            set;
        }

        [ColumnOrder(110)]
        public string PhnNum//PhoneNumber
        {
            get;
            set;
        }

        [ColumnOrder(120)]
        public string FaxNum
        {
            get;
            set;
        }

        [ColumnOrder(130)]
        public string E_Mail
        {
            get;
            set;
        }

        [ColumnOrder(140)]
        public string Region
        {
            get;
            set;
        }

        [ColumnOrder(150)]
        public decimal CstCde//CostCode
        {
            get;
            set;
        }

        [ColumnOrder(160)]
        public bool Selctd//Selected
        {
            get;
            set;
        }

        #endregion
    }
}
