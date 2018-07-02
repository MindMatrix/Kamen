

pipeline {
    agent any

    environment {
        PROJECT = "MindMatrix.Kamen"
        VERSION = "1.0.${BUILD_ID}"
    }

    stages {
        stage("clean") {
            steps {        
                echo 'Build version ' + VersionNumber([versionNumberString : "${VERSION}", projectStartDate : '2017-01-01'])
                bat "git clean -xfd"
                bat "dotnet clean"
            }
        }
        stage("restore") {
            steps {        
                bat "dotnet restore"
            }
        }        
        stage("build") {
            steps {        
                bat "dotnet build -c Release /p:VERSION=${VERSION} /p:SourceLinkCreate=true"
            }
        }        
        stage("test") {
            steps {        
                bat "dotnet test tests\\${PROJECT}.Tests.csproj --no-build -c Release"
            }
        }        
        stage("pack") {
            steps {        
                bat "dotnet pack src\\${PROJECT}.csproj --no-build -c Release /p:VERSION=${VERSION}"
            }
        }        
        stage("push") {
            steps {        
                withCredentials([string(credentialsId: 'NUGET_API_KEY', variable: 'KEY')]) {
                    bat "dotnet nuget push src\\bin\\Release\\${PROJECT}.${VERSION}.nupkg -s https://nuget.amp.vg -k ${KEY}"
                }
            }
        }        
    }
}
