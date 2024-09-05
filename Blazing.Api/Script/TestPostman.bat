@echo off
echo "Iniciando o script..."

postman login --with-api-key {{postman-api-key-here}}
postman collection run 32503863-2a014aa4-e1f2-44c0-a7ae-a13b31414ad3 -i 32503863-234e60fa-caba-4747-be9a-88168bd8e0d8 -i 32503863-c6410acf-457c-4e45-91cc-efb110eb3724 -i 32503863-d7f86b43-847b-4488-9210-a072cd063bc6 -i 32503863-108e1291-0e40-48b4-bcec-d5b55fba5f27 -i 32503863-8746aad2-fd93-4531-b2f2-db8f593e4516 -i 32503863-f02c86da-c9a6-4599-aff7-ec60da7b7f7a
pause >nul

echo "Script concluído."

