FROM gradle:4.9-jdk8-alpine as builder
USER root
WORKDIR /builder
ADD . /builder
RUN gradle build --stacktrace
CMD ["java", "-jar", "./build/libs/gateway-1.0-SNAPSHOT.jar"]