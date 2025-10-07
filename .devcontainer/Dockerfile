FROM mcr.microsoft.com/devcontainers/dotnet:1-9.0-noble

# [Optional] Uncomment this section to install additional OS packages.
RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
    && apt-get -y install --no-install-recommends \ 
    curl unzip

# installing jre for Karate 
# https://stackoverflow.com/a/44154721
RUN apt-get update && \
    DEBIAN_FRONTEND=noninteractive \
    apt-get -y install default-jre-headless && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

RUN curl -o /karate.jar -L 'https://github.com/intuit/karate/releases/download/v1.5.1/karate-1.5.1.jar'

# [Optional] Uncomment this line to install global node packages.
# RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g <your-package-here>" 2>&1