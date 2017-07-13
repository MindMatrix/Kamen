
pipeline {
    agent any

    environment {
        VERSION = "1.0.${BUILD_ID}"
    }

    stages {
        stage("clean") {
            steps {        
                echo 'Build version ' + VersionNumber([versionNumberString : "${VERSION}", projectStartDate : '2017-01-01'])
                bat "build\\clean ${VERSION}"
            }
        }
        stage("restore") {
            steps {        
                bat "build\\restore ${VERSION}"
            }
        }        
        stage("build") {
            steps {        
                bat "build\\build ${VERSION}"
            }
        }        
        stage("test") {
            steps {        
                bat "build\\test ${VERSION}"
            }
        }        
        stage("pack") {
            steps {        
                bat "build\\pack ${VERSION}"
            }
        }        
    }
}