pipeline {
    agent any
    environment {
        VERSION = VersionNumber([versionNumberString : '1.0.${BUILD_ID}', projectStartDate : '2017-01-01'])
    }
    stages {
        stage('Build') {
            steps {
                echo '%VERSION%'
                echo '$env.VERSION'
                echo '${VERSION}'
                echo '${env.VERSION}'
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
