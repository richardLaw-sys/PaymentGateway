using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject.UIModel
{
    public class BaseResponseUIModel
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
