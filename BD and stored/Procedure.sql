

GO
USE BDMasiv
GO
--EXEC UspListRoulette 'ABIERTA'
CREATE PROCEDURE UspListRoulette
	@peState			VARCHAR(10)=''
AS BEGIN
	SELECT RouletteId,Names,State,Active 
	FROM Roulette WHERE (ISNULL(@peState,'')=''  OR State=@peState )
END
GO

CREATE PROCEDURE UspRegisterRoulette
 @peRouletteId		INT=0
,@peNames			VARCHAR(50)=''
--,@peAmountMax		DECIMAL=0
--,@peActive			BIT=0
,@psRouletteId		INT OUTPUT

AS BEGIN
	IF EXISTS(SELECT * FROM Roulette WHERE (UPPER(@peNames))=(UPPER(Names)))
		BEGIN
				SET @psRouletteId=0
		END
	ELSE
		BEGIN
			INSERT INTO Roulette
			(
			 Names	
			,AmountMax
			,State	
			,Active	
			)
			VALUES
			(
			 @peNames
			,10000--@peAmountMax
			,'CREADA'	
			,1		
			)
			SET @psRouletteId=SCOPE_IDENTITY()
		END
		
END;

SELECT *FROM Roulette

GO

CREATE PROCEDURE UspOpenRoulette
  @peRouletteId		INT=0
 ,@psMessage		VARCHAR(10) OUTPUT

AS BEGIN
	
	IF EXISTS(SELECT TOP 1 RouletteId FROM Roulette WHERE RouletteId=@peRouletteId AND State='CREADA')
		BEGIN
			UPDATE Roulette SET State='APERTURADA' WHERE RouletteId=@peRouletteId
			SET @psMessage='EXITOSA'
		END
	ELSE
		BEGIN
			SET @psMessage='DENEGADA'
		END		
END;

GO
/*
DECLARE @vFecha DATETIME=CONVERT(datetime,'16-02-2021')
EXEC UspRegisterBet 0,31,100,@vFecha,3,1,0
*/
GO
CREATE PROCEDURE UspRegisterBet
 @peBetId			INT 	
,@peNumber			INT					
--,@peColor			CHAR(1)				
,@peBetAmount		NUMERIC	
,@peDateAmount		DATETIME
--,@peWinner			BIT
--,@pePaymentxN		DECIMAL(15,2)		
--,@pePaymentxColor	DECIMAL(15,2)				
--,@peState			VARCHAR(10)			
--,@peActive			BIT					
,@peUserId			INT					
,@peRouletteId		INT		
,@psBetId			INT OUTPUT
AS BEGIN

	DECLARE @vClientId		INT=0
	DECLARE @vCreditClient	DECIMAL(18,2)=0
	DECLARE @vAmountMax		DECIMAL(18,2)=0
	DECLARE @vBetTotal		NUMERIC=0
	DECLARE @vColor			CHAR(1)=''
	
	IF (@peNumber% 2= 0)
		BEGIN
			SET @vColor='R'	--Par :Rojo
		END
	ELSE
		BEGIN
			SET @vColor='N'	--Impar : Negro
		END

	--SELECT AmountMax,* FROM Roulette WHERE RouletteId=1
	SELECT @vClientId=ClientId,@vCreditClient=Credit FROM Client WHERE UserId=@peUserId				--Id y linea de credito del cliente
	SELECT @vAmountMax=AmountMax FROM Roulette WHERE RouletteId=@peRouletteId						--monto de apuesta maximo de la ruleta
	SELECT @vBetTotal=ISNULL(SUM(BetAmount),0) FROM Bet WHERE RouletteId=@peRouletteId				--la suma de las apuestas por ruleta

	SELECT  ISNULL(SUM(BetAmount),0) FROM Bet WHERE RouletteId=1

	--verificar que la apuesta sea menor o igual al credito del cliente
	SELECT @peBetAmount '@peBetAmount',@vCreditClient '@vCreditClient',@vAmountMax '@vAmountMax',@vBetTotal '@vBetTotal'
	IF (@peBetAmount<=@vCreditClient)
		BEGIN
			PRINT '1'
			IF(@vBetTotal=0)
				BEGIN
				PRINT 'Total=0'
					INSERT INTO Bet(Number,Color,BetAmount,DateAmount,Winner,PaymentxN,PaymentxColor,State,Active,UserId,RouletteId)
					VALUES(@peNumber,@vColor,@peBetAmount,@peDateAmount,0,5,1.8,'APERTURADA',1,@peUserId,@peRouletteId)
					SET @psBetId=SCOPE_IDENTITY()
					UPDATE Client SET Credit=(Credit-@peBetAmount) WHERE ClientId=@vClientId
				END
			ELSE
				BEGIN
					PRINT 'Total>0'
					IF ((@vBetTotal+@peBetAmount)<=@vAmountMax)
						BEGIN
							PRINT '@vBetTotal>@vAmountMax'
							INSERT INTO Bet(Number,Color,BetAmount,DateAmount,Winner,PaymentxN,PaymentxColor,State,Active,UserId,RouletteId)
							VALUES(@peNumber,@vColor,@peBetAmount,@peDateAmount,0,5,1.8,'APERTURADA',1,@peUserId,@peRouletteId)
							SET @psBetId=SCOPE_IDENTITY()
							UPDATE Client SET Credit=(Credit-@peBetAmount) WHERE ClientId=@vClientId
						END
					ELSE
						BEGIN
							SET @psBetId=0
						END
				END
				
		END
	ELSE
		BEGIN
			SET @psBetId=0
		END		
END;
GO
/*
SELECT * FROM Bet
SELECT * FROM Client
SELECT *FROM Roulette
*/
GO

USE BDMasiv
GO
--EXEC UspCloseRoulette 1
ALTER PROCEDURE UspCloseRoulette
	@peRouletteId INT=0
AS BEGIN

	--Creacion de Tabla Apuestas
	--==========================================================================
	DECLARE @tblBet TABLE(
	 IdTbl			INT IDENTITY(1,1)		NOT NULL	
	,BetId			INT						NOT NULL	
	,Number			INT						NOT NULL	
	,Color			CHAR(1)					NOT NULL
	,BetAmount		NUMERIC					NOT NULL
	,DateAmount		DATETIME				NOT NULL
	,Winner			BIT						NOT NULL
	,PaymentxN		DECIMAL(15,2)			NOT NULL
	,PaymentxColor	DECIMAL(15,2)			NOT NULL
	,TotalxN		DECIMAL(15,2)			NOT NULL
	,TotalxColor	DECIMAL(15,2)			NOT NULL
	,Total			DECIMAL(15,2)			NOT NULL
	,State			VARCHAR(10)				NOT NULL
	,ClientId		INT						NOT NULL
	,UserId			INT						NOT NULL
	,RouletteId		INT						NOT NULL
	)

		---- Creamos las variables
	DECLARE @NroGanador INT;
	DECLARE @NroInicio  INT;
	DECLARE @NroFin  INT
	DECLARE @Estado  VARCHAR(10)


	--SELECT *FROM Ruleta WHERE Estado='CREADA' AND IdRuleta=1
	--Cerrar la ruleta :Cambiar el estado : CERRADO
	--===========================================================================
	SELECT @Estado=State FROM Roulette WHERE RouletteId=@peRouletteId
	IF (@Estado='APERTURADA')
	BEGIN 
	
		UPDATE Roulette SET State='CERRADO' WHERE State='APERTURADA' AND RouletteId=@peRouletteId

		--Seleccionar el numero ganador
		--==========================================================================



		--Esto va a seleccionar un número ganador entre 1 y 36.
		SET @NroFin = 1			---- el mínimo 
		SET @NroInicio = 36		---- el máximo 
		SET @NroGanador = ROUND(((@NroInicio - @NroFin -1) * RAND() + @NroFin), 0)
		--SELECT @NroGanador
		SET @NroGanador =4;

	
		INSERT INTO @tblBet(BetId,Number,Color,BetAmount,DateAmount,Winner,PaymentxN,PaymentxColor,TotalxN,TotalxColor,Total,State,ClientId,UserId,RouletteId)
		SELECT  B.BetId,B.Number,B.Color,B.BetAmount,B.DateAmount,B.Winner,B.PaymentxN,B.PaymentxColor,0,0,0,B.State,(SELECT ClientId FROM Client WHERE UserId=b.UserId),B.UserId,B.RouletteId
		FROM Bet B WHERE B.RouletteId=@peRouletteId AND State='APERTURADA'
		--SELECT * FROM @tblBet

	   	  
		DECLARE @Total INT=0
		DECLARE @Cont  INT=1

		SET @Total =(SELECT COUNT(*) FROM @tblBet)
		--SELECT @Total

		DECLARE  @IdCliente		INT=0
		DECLARE  @IdApuesta		INT=0
		DECLARE  @Nro			INT=0
		DECLARE  @MontoApuesta	NUMERIC=0
		DECLARE  @PagoxNro		DECIMAL(15,2)=0
		DECLARE  @PagoxColor	DECIMAL(15,2)=0

		DECLARE  @Credito		DECIMAL(15,2)=0

		WHILE @Cont<=@Total
		BEGIN
			SELECT @IdCliente=ClientId,@IdApuesta=BetId,@MontoApuesta=BetAmount,@Nro=Number,@PagoxNro=PaymentxN,@PagoxColor=PaymentxColor FROM @tblBet WHERE IdTbl=@Cont
			SELECT @Credito=Credit FROM Client WHERE ClientId=1
		
			IF @NroGanador=@Nro
				BEGIN
					UPDATE @tblBet SET Winner=1,TotalxN=(@MontoApuesta*@PagoxNro),
													  TotalxColor=(@MontoApuesta*@PagoxColor),
													  Total=((@MontoApuesta*@PagoxNro)+(@MontoApuesta*@PagoxColor)) WHERE IdTbl=@Cont
			
					UPDATE Bet SET Winner=1 WHERE BetId=@IdApuesta
					--UPDATE Client SET Credit=(@Credito+@MontoApuesta) WHERE ClientId=@IdCliente
					UPDATE Client SET Credit=(@Credito+((@MontoApuesta*@PagoxNro)+(@MontoApuesta*@PagoxColor))) WHERE ClientId=@IdCliente
				END
		

			SET @Cont=@Cont+1
		END
	END;
		SELECT BetId,Number,Color,BetAmount,DateAmount,Winner,PaymentxN,PaymentxColor,TotalxN,TotalxColor,Total,State,ClientId,UserId,RouletteId FROM @tblBet

END;


SELECT *FROM Roulette
SELECT *FROM Bet