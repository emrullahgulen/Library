using Entity.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebMvc.Models
{
    public class BookTypesListModel
    {
        public BookType SelectedValue { get; set; }
        public IEnumerable<SelectListItem> EnumList { get; set; }
    }
}
