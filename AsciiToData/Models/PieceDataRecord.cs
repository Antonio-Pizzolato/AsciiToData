using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiToData.Models
{
    public class PieceDataRecord : CMP265Record
    {
        public double? ExLength { get; set; }
        public double? InLength { get; set; }
        public double? LTAngle { get; set; }
        public double? LPAngle { get; set; }
        public double? RTAngle { get; set; }
        public double? RPAngle { get; set; }
        public double? NPieces { get; set; }
        public double? NCarriage { get; set; }
        public double? NSlot { get; set; }
        public double? UniquePiece { get; set; }
        public string? PieceID { get; set; }
        public string? SpecialCut { get; set; }
        public string? Order { get; set; }
        public string? Customer { get; set; }
        public string? PosPlacCross1 { get; set; }
        public string? PosPlacCross2 { get; set; }
        public string? PosPlacCross3 { get; set; }
        public string? Reinforce { get; set; }
        public string? Fixing { get; set; }
        public string? TypeCode { get; set; }
        public string? HolesH2o1 { get; set; }
        public string? HolesH2o2 { get; set; }
        public string? HolesH2o3 { get; set; }
        public string? Notes { get; set; }
        public double? Q1 { get; set; }
        public double? Q2 { get; set; }
        public double? Q3 { get; set; }
        public double? Q4 { get; set; }
        public string? Info1 { get; set; }
        public string? Info2 { get; set; }
        public string? Info3 { get; set; }
        public string? Info4 { get; set; }
        public string? Info5 { get; set; }

    }
}
