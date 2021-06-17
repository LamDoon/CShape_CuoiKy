namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [DisplayName("Mã sản phẩm")]
        public int ID { get; set; }

        [StringLength(50)]
        [DisplayFormat(NullDisplayText ="Tên sản phẩm không thể trống!")]
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }

        [DisplayName("Đơn giá")]
        public decimal? UnitCost { get; set; }

        [DisplayName("Số lượng còn")]
        public int? Quantity { get; set; }

        [StringLength(250)]
        [DisplayName("Hình ảnh")]
        public string Image { get; set; }

        [StringLength(250)]
        [DisplayName("Mô tả")]
        public string Descreption { get; set; }

        [StringLength(250)]
        [DisplayName("Đã bán")]
        public string Status { get; set; }

        [DisplayName("Loại vải")]
        public int? IDType { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}
