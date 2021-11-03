namespace Feliz.ViewEngine.Htmx

open Feliz.ViewEngine

type HyperscriptBuilder() = 
    let commands = ResizeArray<string>()

    member this.click() = 
        commands.Add "click"
        this

    member this.keyup() = 
        commands.Add "keyup"
        this

    member this.mouseenter() = 
        commands.Add "mouseenter"
        this

    member this.mouseleave() = 
        commands.Add "mouseleave"
        this

    member this.take(content: string) =
        commands.AddRange [ "take"; content ]
        this

    member this.for'(content: string) = 
        commands.AddRange [ "for"; content ]
        this

    member this.put(content: string) = 
        commands.AddRange [ "put"; content ]
        this

    member this.put() = 
        commands.Add "put"
        this

    member this.into(content: string) = 
        commands.AddRange [ "into"; content ]
        this

    member this.the() = 
        commands.Add "the"
        this

    member this.closest(content: string) = 
        commands.AddRange [ "closest"; content ]
        this

    member this.first(content: string) = 
        commands.AddRange [ "first"; content ]
        this

    member this.next(content: string) = 
        commands.AddRange [ "next"; content ]
        this

    member this.wait(content: string) = 
        commands.AddRange [ "wait"; content ]
        this

    member this.waitSeconds(seconds: int) = 
        commands.AddRange [ "wait"; string seconds + "s" ]
        this

    member this.waitMilliseconds(milliseconds: int) = 
        commands.AddRange [ "wait"; string milliseconds + "ms"]
        this

    /// <summary>Alias for then</summary>
    member this.afterwards() = 
        commands.Add "then"
        this

    member this.then'() = 
        commands.Add "then"
        this

    member this.transition() = 
        commands.Add "transition"
        this

    member this.opacity() = 
        commands.Add "opacity"
        this

    member this.to'(content: string) = 
        commands.AddRange ["to"; content] 
        this

    member this.to'(content: int) = 
        commands.AddRange ["to"; string content] 
        this

    member this.add(content: string) = 
        commands.AddRange [ "add"; content ]
        this

    member this.change() =
        commands.Add "change"
        this

    member this.remove(content: string) = 
        commands.AddRange [ "remove"; content ]
        this

    member this.every() = 
        commands.Add "every"
        this

    member this.on() = 
        commands.Add "on"
        this

    member this.on(content: string) = 
        commands.AddRange [ "on"; content ]
        this

    member this.fetch(url: string) = 
        commands.AddRange [ "fetch"; url ]
        this

    member this.toggle(content: string) = 
        commands.AddRange [ "toggle"; content ]
        this

    member this.until(content: string) = 
        commands.AddRange [ "until"; content ]
        this
    
    member this.set(content: string) = 
        commands.AddRange [ "set"; content ]
        this

    member this.or'() = 
        commands.Add "or"
        this

    member this.touchbegin() = 
        commands.Add "touchbegin"
        this

    member this.end'() = 
        commands.Add "end"
        this

    member this.throttled() = 
        commands.Add "throttled"
        this

    member this.debounced() = 
        commands.Add "debounced"
        this

    member this.at(content: string) = 
        commands.AddRange ["at"; content]
        this

    member this.at(millesconds: int) = 
        commands.AddRange ["at"; string millesconds + "ms"]
        this

    member this.it() = 
        commands.Add "it"
        this

    member this.serialize() = String.concat " " commands

[<AutoOpen>]
module HypertextExtensions = 
    let on() = HyperscriptBuilder().on()
    let onEvent(ev: string) = HyperscriptBuilder().on(ev)

type hx =
    /// <summary>
    /// Will cause an element to issue a POST to the specified URL and swap the HTML into the DOM using a swap strategy.
    /// </summary>
    static member post(url) = prop.custom("hx-post", url)
    /// <summary>
    /// The hx-delete attribute will cause an element to issue a DELETE to the specified URL and swap the HTML into the DOM using a swap strategy.
    /// </summary>
    static member delete(url) = prop.custom("hx-delete", url)
    /// <summary>
    /// The hx-get attribute will cause an element to issue a GET to the specified URL and swap the HTML into the DOM using a swap strategy.
    /// </summary>
    static member get(url) = prop.custom("hx-get", url)
    /// <summary> 
    /// The hx-boost attribute allows you to "boost" normal anchors and form tags to use AJAX instead. This has the nice fallback that, if the user does not have javascript enabled, the site will continue to work
    /// For anchor tags, clicking on the anchor will issue a GET request to the url specified in the href and will push the url so that a history entry is created. The target is the body tag, and the innerHTML swap strategy is used by default. All of these can be modified by using the appropriate attributes, except the click trigger.
    /// For forms the request will be converted into a GET or POST, based on the method in the method attribute and will be triggered by a submit. Again, the target will be the body of the page, and the innerHTML swap will be used.
    /// </summary>
    static member boost(value: bool) = prop.custom("hx-boost", value.ToString().ToLower())
    /// <summary>
    /// Allows you to specify how the response will be swapped in relative to the target of an AJAX request.
    /// </summary>
    static member swap(content) = prop.custom("hx-swap", content)
    /// <summary>
    /// The hx-confirm attribute allows you to confirm an action before issuing a request. This can be useful in cases where the action is destructive and you want to ensure that the user really wants to do it.
    /// </summary>
    static member confirm(message:string) = prop.custom("hx-confirm", message)
    /// <summary>
    /// The hx-disable attribute will disable htmx processing for a given element and all its children. This can be useful as a backup for HTML escaping, when you include user generated content in your site, and you want to prevent malicious scripting attacks.
    /// </summary>
    static member disabled(value: bool) = prop.custom("hx-disabled", value.ToString().ToLower())
    /// <summary>
    /// The hx-target attribute allows you to target a different element for swapping than the one issuing the AJAX request. The value of this attribute can be:
    /// a CSS query selector of the element to target
    /// this which indicates that the element that the hx-target attribute is on is the target
    /// closest [CSS selector] which will find the closest parent ancestor that matches the given CSS selector. (e.g. closest tr will target the closest table row to the element)
    /// find [CSS selector] which will find the first child descendant element that matches the given CSS selector. (e.g find tr will target the first child descendant row to the element)
    /// </summary>
    static member target(selector: string) = prop.custom("hx-target", selector)
    /// <summary>
    /// The hx-trigger attribute allows you to specify what triggers an AJAX request. A trigger value can be one of the following:    /// An event name (e.g. "click" or "my-custom-event") followed by an event filter and a set of event modifiers
    /// A polling definition of the form every [timing declaration]
    /// A comma-separated list of such events
    /// </summary>
    static member trigger(trigger: string) = prop.custom("hx-trigger", trigger)
    /// <summary>
    /// The hx-ws allows you to work with Web Sockets directly from HTML. The value of the attribute can be one or more of the following, separated by commas:
    /// connect:[url] or connect:[prefix]:[url] - A URL to establish an WebSocket connection against.
    /// Prefixes ws or wss can optionally be specified. If not specified, HTMX defaults to add the location's scheme-type, host and port to have browsers send cookies via websockets.
    /// send - Sends a message to the nearest websocket based on the trigger value for the element (either the natural event of the event specified by [hx-trigger])
    /// </summary>
    static member ws(socket: string) = prop.custom("hx-ws", socket)
    /// <summary>
    /// The hx-headers attribute allows you to add to the headers that will be submitted with an AJAX request.
    /// By default, the value of this attribute is a list of name-expression values in JSON (JavaScript Object Notation) format.
    /// If you wish for hx-headers to evaluate the values given, you can prefix the values with javascript: or js:
    /// </summary>
    static member headers(value: string) = prop.custom("hx-headers", value)
    /// <summary>
    /// The hx-headers attribute allows you to add to the headers that will be submitted with an AJAX request.
    /// By default, the value of this attribute is a list of name-expression values in JSON (JavaScript Object Notation) format.
    /// If you wish for hx-headers to evaluate the values given, you can prefix the values with javascript: or js:
    /// </summary>
    static member headers(values: (string * string) list) = 
        let headersDict = 
            values
            |> List.map (fun (key, value) -> sprintf "\"%s\": \"%s\"" key value)
            |> String.concat ", "

        prop.custom("hx-headers", sprintf "{%s}" headersDict)

    /// <summary>
    /// The hx-indicator attribute allows you to specify the element that will have the htmx-request class added to it for the duration of the request. This can be used to show spinners or progress indicators while the request is in flight.
    /// The value of this attribute is a CSS query selector of the element or elements to apply the class to, or the keyword closest, followed by a CSS selector, which will find the closest matching parent (e.g. closest tr);
    /// Here is an example with a spinner adjacent to the button:
    /// </summary>
    static member indicator(value: string) = prop.custom("hx-indicator", value)
    /// <summary>
    /// The hx-params attribute allows you to filter the parameters that will be submitted with an AJAX request.
    
    /// The possible values of this attribute are:
    ///
    /// * - Include all parameters (default)
    /// 
    ///  none - Include no parameters
    ///
    /// not [param-list] - Include all except the comma separated list of parameter names
    ///
    /// [param-list] - Include all the comma separated list of parameter names
    /// </summary>
    static member parameters(value: string) = prop.custom("hx-params", value)
    /// <summary>
    /// The hx-preserve attribute allows you to keep a section of content unchanged between HTML replacement. When hx-preserve is set to true, an element is preserved (by id) even if the surrounding HTML is updated by htmx. An element must have an id to be preserved properly.
    /// </summary>
    static member preserve(value: bool) = prop.custom("hx-preserve", value.ToString().ToLower())
    /// <summary>
    /// The hx-prompt attribute allows you to show a prompt before issuing a request. The value of the prompt will be included in the requst in the HX-Prompt header.
    /// </summary>
    static member prompt(message: string) = prop.custom("hx-prompt", message)
    /// <summary>
    /// The hx-push-url attribute allows you to "push" a new entry into the browser location bar, which creates a new history entry, allowing back-button and general history navigation. The possible values of this attribute are true, false or a custom string.
    /// </summary>
    static member pushUrl(value: bool) = prop.custom("hx-push-url", value.ToString().ToLower())
    /// <summary>
    /// The hx-select attribute allows you to select the content you want swapped from a response. The value of this attribute is a CSS query selector of the element or elements to select from the response.
    /// </summary>
    static member select(value: string) = prop.custom("hx-select", value)
    /// <summary>
    /// The hx-sse allows you to work with Server Sent Event EventSources directly from HTML. The value of the attribute can be one or more of the following, separated by white space:
    ///
    /// connect:[url] - A URL to establish an EventSource against
    /// 
    /// swap:[eventName] - Swap SSE message content into a DOM node on matching event names
    /// </summary>
    static member sse(value: string) = prop.custom("hx-sse", value)
    /// <summary>
    /// The hx-swap-oob attribute allows you to specify that some content in a response should be swapped into the DOM somewhere other than the target, that is "Out of Band". This allows you to piggy back updates to other element updates on a response.
    /// </summary>
    static member swapOob(value: string) = prop.custom("hx-swap-oob", value)
    /// <summary>
    /// The hx-vals attribute allows you to add to the parameters that will be submitted with an AJAX request.
    ///
    /// By default, the value of this attribute is a list of name-expression values in JSON (JavaScript Object Notation) format.
    /// 
    /// If you wish for hx-vals to evaluate the values given, you can prefix the values with javascript: or js:.
    /// </summary>
    static member vals(value: string) = prop.custom("hx-vals", value)
    /// <summary>
    /// The hx-vals attribute allows you to add to the parameters that will be submitted with an AJAX request.
    ///
    /// By default, the value of this attribute is a list of name-expression values in JSON (JavaScript Object Notation) format.
    /// 
    /// If you wish for hx-vals to evaluate the values given, you can prefix the values with javascript: or js:.
    /// </summary>
    static member vals(values: (string * string) list) = 
        let valuesDict = 
            values
            |> List.map (fun (key, value) -> sprintf "\"%s\": \"%s\"" key value)
            |> String.concat ", "

        prop.custom("hx-vals", sprintf "{%s}" valuesDict)

    /// <summary>
    /// HyperScript content
    /// </summary>
    static member hyperscript(script: string) = prop.custom("_", script)
    /// <summary>
    /// HyperScript content
    /// </summary>
    static member hyperscript(script: HyperscriptBuilder) = prop.custom("_", script.serialize())
    /// <summary>
    /// HyperScript content
    /// </summary>
    static member hyperscript(scripts: HyperscriptBuilder list) = 
        let content = 
            scripts
            |> List.map (fun script -> script.serialize())
            |> String.concat " "
        prop.custom("_", content)
    /// <summary>
    /// HyperScript content
    /// </summary>
    static member hyperscript(script: string list) = prop.custom("_", String.concat " " script)

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