workspace "Name" "Description" {

    !identifiers hierarchical

    model {
        u = person "User"{
            description "Person who wants to check the weather forecast"
            }
            ss = softwareSystem "Breeze" {
                description "provides real-time, hyper-local forecasts with ease"
                wa = container "Web Application"{
                    description "Provides all the platform functionality via a web browser"
                    auth = component "Authentication Module"{
                        description "Allows users to signup/login into the app"
                    }
                    forecast = component "Forecast Module"{
                         description "Allows users to view the weather forecast"
                    }
                    location = component "Location Selection Module"{
                         description "Allows users to select the prefered location"
                    }
                }
                api = container "Api application"{
                    tags "Provides user requests via a JSON/HTTP API"
                }
                db = container "Database Schema" {
                    tags "Database"
                }
          
        }
        a = softwareSystem "ANM's API"{
            description "API from the National Meteorological Administration of Romania"
            }
        u -> ss.wa "Uses"
        ss.wa -> a "requests weather data from"
        a -> ss.wa "provides data to"
        
        ss.wa -> ss.api "makes HTTP/HTTPS requests"
        ss.api -> ss.db "queries and updates"
        
        u -> ss.wa.auth "authenticates via"
        u -> ss.wa.forecast "sees forecast via"
        u -> ss.wa.location "selects location via"
        ss.wa.auth -> ss.api "makes http request to"
        ss.wa.forecast -> ss.api "makes http request to"
        ss.wa.location -> ss.api "makes http request to"
    }

    views {
        systemContext ss "Diagram1" {
            include *
            autolayout lr
        }

        container ss "Diagram2" {
            include *
            autolayout lr
        }
        component ss.wa "Diagram3" {
            include *
            autolayout lr
        }

        styles {
            element "Element" {
                color #ffffff
            }
            element "Person" {
                background #199b65
                shape person
            }
            element "Software System" {
                background #1eba79
            }
            element "Container" {
                background #1eba79
            }
            element "Database" {
                shape cylinder
            }
            element "Component" {
                background #0000ff
            }
        }
    }

    configuration {
        scope softwaresystem
    }

}
