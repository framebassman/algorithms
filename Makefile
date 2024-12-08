new-lc-%:
	cd leetcode && dotnet new xunit --name $*

new-hr-%:
	cd hackerrank && dotnet new xunit --name $*

new-pg-%:
	cd playground && dotnet new xunit --name $*

rm-vscode-files:
	rm .vscode/launch.json
	rm .vscode/tasks.json
