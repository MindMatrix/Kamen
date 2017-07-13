pipeline {
    agent any
    environment {
        VERSION_MAJOR = '1'
        VERSION_MINOR = '0'
        VERSION = '0.0.0'
    }
    stages {
        stage('Setup'){
            steps{
                script{
                    VERSION = VersionNumber([versionNumberString : '$(env.VERSION_MAJOR).$(env.VERSION_MINOR).${BUILD_ID}', projectStartDate : '2017-01-01'])
                }
            }
        }
        stage('Build') {
            steps {
                echo '%VERSION%'
                echo '$VERSION'
                echo '$(VERSION)'
                echo $env.VERSION
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
