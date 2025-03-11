using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    public class PezzoRecord
    {
        public float? l_ext;
        public float? l_int;
        public float? angt_sx;
        public float? angp_sx;
        public float? angt_dx;
        public float? angp_dx;

        public short? num_pezzo;
        public short? n_carrello;
        public short? n_slot;

        public long? n_unico_pezzo;

        public string? id;
        public string? tag_speciale;
        public string? ordine;
        public string? cliente;

        public string? pian_trav1;
        public string? pian_trav2;
        public string? pian_trav3;

        public string? rinf;
        public string? fissaggio;
        public string? cod_tip;

        public string? f_acqua1;
        public string? f_acqua2;
        public string? f_acqua3;

        public string? note;

        public float? q1;
        public float? q2;
        public float? q3;
        public float? q4;

        public string? info1;
        public string? info2;
        public string? info3;
        public string? info4;
        public string? info5;
    }
}
