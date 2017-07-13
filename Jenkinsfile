pipeline {
    agent any
    environment {
        VERSION = VersionNumber([
            versionNumberString : '${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_ID}',
            projectStartDate : '2017-01-01'
        ]);
        HELLO = "WORLD"
    }

    stages {
        stage('Build') {
            steps {
                bat 'set'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}