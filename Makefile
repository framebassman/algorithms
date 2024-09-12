new-lc-%:
	cd leetcode && dotnet new xunit --name $*

new-hr-%:
	cd hackerrank && dotnet new xunit --name $*
