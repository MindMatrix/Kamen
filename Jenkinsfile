pipeline {
    agent any
    environment {
        VERSION = '1.0.0'
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
