pipeline {
    agent any
    environment {
        HELLO = VersionNumber([
            versionNumberString : '${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_ID}',
            projectStartDate : '2017-01-01'
        ])
    }

    stages {
        stage('Build') {
            steps {
                echo VersionNumber([
            versionNumberString : '${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_ID}',
            projectStartDate : '2017-01-01'
        ])
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