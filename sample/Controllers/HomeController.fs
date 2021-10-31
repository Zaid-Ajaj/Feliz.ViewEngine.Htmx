namespace App.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Feliz.ViewEngine
open Feliz.ViewEngine.Htmx

type HomeController (logger : ILogger<HomeController>) =
    inherit Controller()

    member private this.Render(html: ReactElement) = 
        let htmlContent = Render.htmlView html
        this.Content(htmlContent, "text/html")

    member private this.Partial(html: ReactElement list) = 
        let htmlContent = Render.htmlView (React.fragment html)
        this.Content(htmlContent, "text/html")

    member private this.MainLayout (body: ReactElement list) =  
        let mainLayout = Html.html [
            Html.head [
                Html.title "F# ♥ Htmx"
                Html.script [ prop.src "https://unpkg.com/htmx.org@1.6.0" ]
                Html.meta [ prop.charset.utf8 ]
            ]

            Html.body body
        ]

        this.Render(mainLayout)

    member this.Clicked() = this.Partial [
        Html.p "Content retrieved by HTMX"
    ]

    member this.Index() = this.MainLayout [
        Html.h1 "Home"
        Html.button [
            hx.get "/Home/Clicked"
            hx.swap.outerHTML
            prop.text "Click me!"
        ]
    ]