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

    member private this.Parial(html: ReactElement list) = 
        let htmlContent = Render.htmlView (React.fragment html)
        this.Content(htmlContent, "text/html")

    member private this.Path(method: string) =
        let controllerName = nameof(HomeController).Replace(nameof(Controller), String.Empty)
        $"/{controllerName}/{method}"

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

    member this.Clicked() = this.Parial [
        Html.p "Content retrieved by HTMX"
    ]

    member this.Index() = this.MainLayout [
        Html.h1 "Home"
        Html.button [
            hx.post "/Home/Clicked"
            hx.swap.outerHTML
            prop.text "Click me!"
        ]
    ]


