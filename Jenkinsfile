node {
	stage 'Checkout'
		checkout scm

	stage 'Build'
		bat 'nuget restore OCR_API.sln'
		bat "\"${tool 'MSBuild'}\" OCR_API.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"

	stage 'Archive'
		archive 'OCR_API/bin/Release/**'

}