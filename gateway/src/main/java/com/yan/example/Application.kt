package com.yan.example

import org.springframework.boot.SpringApplication
import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.context.properties.ConfigurationProperties
import org.springframework.boot.context.properties.EnableConfigurationProperties
import org.springframework.cloud.gateway.route.RouteLocator
import org.springframework.cloud.gateway.route.builder.RouteLocatorBuilder
import org.springframework.context.annotation.Bean
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RestController

@SpringBootApplication
@EnableConfigurationProperties(UriConfiguration::class)
@RestController
open class Application {
    @Bean
    open fun myRoutes(builder: RouteLocatorBuilder, config: UriConfiguration): RouteLocator {
        val uri = config.sampleServer
        println(uri)
        return builder.routes()
            .route {
                it
                    .path("/api/weatherforecast")
                    .uri(uri)
            }
            .build()
    }

    @GetMapping("/fallback")
    fun fallback(): String = "hello"
}

fun main(args: Array<String>) {
    SpringApplication.run(Application::class.java, *args)
}

@ConfigurationProperties
class UriConfiguration {
    var sampleServer = "http://someapi:80"
}
