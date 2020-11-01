using System;
using System.Text;
using System.Web.Mvc;
using BooksStore.WebUI.Models;

namespace BooksStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(
        this HtmlHelper html,
        PagingInfo pagingInfo,
        Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            TagBuilder back = new TagBuilder("li"); // Construct an <a> tag
            TagBuilder back_ref = new TagBuilder("a");
            back_ref.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage-1));
            back_ref.InnerHtml = "&laquo";
            back_ref.AddCssClass("prev");
            TagBuilder forward = new TagBuilder("li"); // Construct an <a> tag
            TagBuilder forward_ref = new TagBuilder("a");
            forward_ref.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
            forward_ref.InnerHtml = "&raquo";
            forward_ref.AddCssClass("next");
            if (pagingInfo.CurrentPage >1)
                result.Append(back_ref.ToString());
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {

                TagBuilder li = new TagBuilder("li"); // Construct an <a> tag
                TagBuilder tag = new TagBuilder("a"); 
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("active");
                result.Append(li.ToString()+ tag.ToString()+" ");
            }
            if(pagingInfo.CurrentPage<pagingInfo.TotalPages)
            result.Append(forward_ref.ToString());

            return MvcHtmlString.Create(result.ToString());
        }

    }
}