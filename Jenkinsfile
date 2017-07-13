pipeline {
    environment {
        FOO = "BAR"
        VERSION = "1.0.${BUILD_ID}"
    }

    agent { label "master" }

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
                bat "build\\restore ${VERSION}"
            }
        }        
        stage("test") {
            steps {        
                bat "build\\restore ${VERSION}"
            }
        }        
        stage("pack") {
            steps {        
                bat "build\\restore ${VERSION}"
            }
        }        
    }
}