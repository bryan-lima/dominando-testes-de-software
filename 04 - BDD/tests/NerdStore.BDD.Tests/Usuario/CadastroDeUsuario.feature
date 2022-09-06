Funcionalidade: Usuário - Cadastro
	Como um visitante da Loja
	Eu desejo me cadastrar como usuário
	Para que eu possa realizar compras na Loja

Cenário: Cadastro de usuário com sucesso
Dado Que o visitante está acessando o site da loja
Quando Ele clicar em registrar
E Preencher os dados do formulário
		| Dados                |
		| E-mail               |
		| Senha                |
		| Confirmação da Senha |
E Clicar no botão registrar
E Ele será redirecionado para página de confirmação de conta
E Clicar no link para confirmar conta
Então Será exibido uma mensagem de conta confirmada

Cenário: Cadastro com senha sem maiúsculas
Dado Que o visitante está acessando o site da loja
Quando Ele clicar em registrar
E Preencher os dados do formulário com uma senha sem maiúsculas
		| Dados                |
		| E-mail               |
		| Senha                |
		| Confirmação da Senha |
E Clicar no botão registrar
Então Ele receberá uma mensagem de erro que a senha precisa conter uma letra maiúscula

Cenário: Cadastro com senha sem caractere especial
Dado Que o visitante está acessando o site da loja
Quando Ele clicar em registrar
E Preencher os dados do formulário com uma senha sem caractere especial
		| Dados                |
		| E-mail               |
		| Senha                |
		| Confirmação da Senha |
E Clicar no botão registrar
Então Ele receberá uma mensagem de erro que a senha precisa conter um caractere especial