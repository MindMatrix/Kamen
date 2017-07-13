pipeline {
    agent any
    environment {
        VERSION = VersionNumber([versionNumberString : '1.0.${BUILD_ID}', projectStartDate : '2017-01-01'])
    }
    stages {
        stage('Build') {
            steps {
                bat 'echo %VERSION%'
                bat 'echo $env.VERSION'
                bat 'echo ${VERSION}'
                bat 'echo $(VERSION)'
                bat 'echo $(VERSION}'
                bat 'echo $(env.VERSION)'
                bat 'echo ${env.VERSION}'
                bat 'echo $(env.VERSION}'
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
