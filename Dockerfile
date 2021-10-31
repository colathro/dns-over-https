FROM mcr.microsoft.com/dotnet/aspnet:6.0.0-rc.2-focal

WORKDIR app/

COPY --chown=myuser:myuser ./bin/Debug/net6.0 .
COPY --chown=myuser:myuser init_container.sh .

EXPOSE 80 443

RUN chmod 755 init_container.sh
ENTRYPOINT ["./init_container.sh"]