namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        [StringLength(10)]
        [DisplayName("Mã người dùng")]
        public string ID { get; set; }

        [StringLength(50)]
        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        [StringLength(250)]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [StringLength(50)]
        [DisplayName("Tình trạng")]
        public string Status { get; set; }
    }
}
