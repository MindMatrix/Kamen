pipeline {
    agent any
    environment {
        VERSION_MAJOR = '1'
        VERSION_MINOR = '0'
    }
    stages {
        stage('Setup'){
            steps{
                script{
                    def v = VersionNumber([versionNumberString : '$(VERSION_MAJOR).$(VERSION_MINOR).${BUILD_ID}', projectStartDate : '2017-01-01'])
                }
                bat 'set VERSION = $(v)'
            }
        }
        stage('Build') {
            steps {
                echo '%VERSION%'
            }
        }
        stage('Test') {
            steps {
                bat 'set'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}
