# Variables
$resourceGroupName = "dapr-playgorund"
$storageAccountName = "mystorageaccount$(Get-Random)"  # Ensures a unique name
$location = "WestEurope"
$daprConfigFile = "./components/dapr-state.yml"  # Path to your dapr-state.yml file

# Login to Azure (you might want to run this manually or handle authentication in some other way)
az login

# Create a Resource Group (if needed)
az group create --name $resourceGroupName --location $location

# Create a Storage Account
az storage account create --name $storageAccountName --resource-group $resourceGroupName --location $location --sku Standard_LRS

# Get the Storage Account Key
$storageAccountKey = az storage account keys list --resource-group $resourceGroupName --account-name $storageAccountName --query "[0].value" -o tsv

# Load the dapr-state.yml file content
$daprConfigContent = Get-Content $daprConfigFile

# Replace STORAGE_ACCOUNT_KEY with the actual storage account key
$daprConfigContent = $daprConfigContent -replace "STORAGE_ACCOUNT_KEY", $storageAccountKey

# Save the updated configuration back to the dapr-state.yml file
$daprConfigContent | Set-Content $daprConfigFile

Write-Host "Storage account created and key added to dapr-state.yml."
