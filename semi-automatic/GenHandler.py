#!/usr/bin/python
#coding:utf-8
import sys
__author__='zhangtai'
 
def exec():
	#读取template
	file_AMActorRpcHandler_object = open('AMActorRpcHandler.template','r')
	file_AMessagehandler_object = open('AMessagehandler.template','r')
	file_AMRpcHandler_object = open('AMRpcHandler.template','r')
	file_IActorHandler_object = open('IActorHandler.template','r')
	#读取handler列表
	file_handlers_object = open('HandlerManifest.txt','r')
	
	
	try:
		#template
		actorRpcTpl = file_AMActorRpcHandler_object.read()
		aTpl = file_AMessagehandler_object.read()
		aRpcTmp = file_AMRpcHandler_object.read()
		aActorTmp = file_IActorHandler_object.read()
		#hadlers
		hadlers = file_handlers_object.readlines()
		#处理每个handler
		for line in hadlers:
			#C2G_EnterRoomHandler_RPC
			#ActorRpc
			if line.count('RPC')>0 and line.count('Actor')>0:
				genHandler(line,actorRpcTpl)
			#RPC
			elif line.count('RPC')>0 and line.count('Actor')<=0:
				genHandler(line,aRpcTmp)
			#Actor
			elif line.count('RPC')<=0 and line.count('Actor')>0:
				genHandler(line,aActorTmp)
			#Handler
			elif line.count('RPC')<=0 and line.count('Actor')<=0:
				genHandler(line,aTpl)
			
		
		
	finally:
		file_AMActorRpcHandler_object.close()
		file_AMessagehandler_object.close()
		file_AMRpcHandler_object.close()
		file_IActorHandler_object.close()
		file_handlers_object.close()

	
def	genHandler(handlerName,tmplate):
	handlerName = handlerName.replace('\n','')
	entityName = ''
	smallEntityName=''
	protoReuestName=''
	protoResponseName=''
	#Actor才有entityname
	if handlerName.count('Actor')>0:
		entityName = handlerName.split('_')[2]
		smallEntityName =entityName.lower()
	#获取request
	protoReuestName = handlerName.replace('_RPC','')
	protoReuestName = protoReuestName.replace('Handler','')
	#获取response 
	strArr = protoReuestName.split('_')
	#print('requestName:'+protoReuestName)
	dirstr = ''
	if len(strArr) == 4:
		dirstr = strArr[1]
	elif len(strArr) == 2:
		dirstr = strArr[0]
	else:
		print('handlerName：'+protoReuestName +' 格式不正确，请检查后处理。')
		return
	#print('>>>>>>>>>>>>>>dirstr:'+dirstr+'. len(strArr):'+str(len(strArr)))
	#修复可以多字符描述服务器名称
	finrstSp = dirstr.find('2')
	lastSp = dirstr.rfind('2')
	#C
	first = dirstr[0:finrstSp]
	#G
	last = dirstr[(lastSp+1):]
	#C2
	newdirstr = dirstr.replace(last,'')
	#G2
	newdirstr = newdirstr.replace(first,last)
	#G2C
	newdirstr = newdirstr + first
	#替换模板中内容
	protoResponseName = protoReuestName.replace(dirstr,newdirstr)
	handlerName = handlerName.replace('_RPC','')
	tmplate = tmplate.replace('[EntityName]',entityName)
	tmplate = tmplate.replace('[SmallEntityName]',smallEntityName)
	tmplate = tmplate.replace('[ProtoReuestName]',protoReuestName)
	tmplate = tmplate.replace('[ProtoResponseName]',protoResponseName)
	tmplate = tmplate.replace('[HandlerName]',handlerName)
	#写入文件
	f = open('gen/'+handlerName+'.cs','w')
	try:
		f.write(tmplate)
	finally:
		f.close()
	
	
		
if __name__ == "__main__": 
	exec()
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	