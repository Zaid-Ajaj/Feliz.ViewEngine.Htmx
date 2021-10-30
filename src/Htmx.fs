namespace Feliz.ViewEngine.Htmx

open Feliz.ViewEngine

type hx =
    /// <summary>
    /// Will cause an element to issue a POST to the specified URL and swap the HTML into the DOM using a swap strategy:
    /// </summary>
    static member post(url) = prop.custom("hx-post", url)
    /// <summary>
    /// Allows you to specify how the response will be swapped in relative to the target of an AJAX request.
    /// </summary>
    static member swap(content) = prop.custom("hx-swap", content)

module hx = 
    /// <summary>
    /// Allows you to specify how the response will be swapped in relative to the target of an AJAX request.
    /// </summary>
    type swap = 
        /// <summary>Replace the entire target element with the response</summary>
        static member outerHTML = prop.custom("hx-swap", "outerHTML")
        /// <summary>The default, replace the inner html of the target element</summary>
        static member innerHTML = prop.custom("hx-swap", "innerHTML")
        /// <summary>Insert the response before the target element</summary>
        static member beforebegin = prop.custom("hx-swap", "beforebegin")
        /// <summary>Insert the response before the first child of the target element</summary>
        static member afterbegin = prop.custom("hx-swap", "afterbegin")
        /// <summary>Insert the response after the last child of the target element</summary>
        static member beforeend = prop.custom("hx-swap", "beforeend")
        /// <summary>Insert the response after the target element</summary>
        static member afterend = prop.custom("hx-swap", "afterend")
        /// <summary>Does not append content from response (out of band items will still be processed)</summary>
        static member none = prop.custom("hx-swap", "none")