#include<stdio.h>
#include<malloc.h>

#define  TRUE     1
#define  FALSE    0
#define  OK           1
#define  ERROR   0
#define  INFEASIBLE  -1
#define  OVERFLOW  -2
typedef  int  Status;   

typedef  char ElemType;


typedef  struct LNode{
	ElemType	data;
	struct  LNode  *next;
}LNode,   *LinkList;

//（1）定义初始化单链表函数
Status InitList_L(LinkList &L)
 {
	L=(LinkList)malloc(sizeof(LNode)) ;
	L->next=NULL;
	printf("（1）初始化单链表已完成！\n");
	return OK;
}


//（2）定义在表尾插入元素函数
Status ListInsert_L(LinkList L, int i, ElemType e) 
{
	LinkList p = L;   
	int j = 0;
	while (p && j < i-1)
	{
		p = p->next; 
		++j; 
	}
	if (!p || j > i-1) return ERROR; 
	LinkList s = (LinkList) malloc (sizeof(LNode)); 
	s->data = e;  
	s->next = p->next;
	p->next = s;
	return OK;
}


//（3）定义输出单链表函数
void DispList_L(LinkList L) 
{
	LinkList p=L->next;
	while (p!=NULL)
	{
		printf("%c\n",p->data);
		p=p->next;
	}

}


//（4）定义输出单链表长度的函数
Status listLength_L(LinkList &L)  
{  
       LinkList p=L->next;  
       int length=0;  
       while(p!=NULL)  
       {  
              length++;  
              p=p->next;  
       }  
	   printf("（4）输出单链表长度已完成：\nlistLength=%d\n",length);
       return length;
}


//（5）定义判断单链表是否为空的函数
bool Is_Empty(LinkList &L)
{
    printf("（5）判断单链表L是否为空,已完成！\n");
	if (L->next == NULL)
    {
        printf ("经判断，链表为空！\n");
        return true;
    } 
	else
    {
		printf ("经判断，链表不为空！\n");
        return false;
    }
}


//（6）定义输出单链表L的第i个元素
Status GetElem_L(LinkList L, int i, ElemType e)
{
	LinkList p=L->next;
	int j=1;
	while(p&&j<i)
	{
		p=p->next;
		++j;
	}
	if(!p||j>i)
		return ERROR;
	e=p->data;
	printf("第%d个元素为%c\n",i,e);
	return OK;

}

//（7）定义输出单链表中某元素的位置的函数
Status LocateElem_L(LinkList L,ElemType e)
{
	int i=1;
	if(!L)
		return ERROR;
	L=L->next;
    while(L)
	{
		if(L->data==e)
			printf("单链表L中元素c的位置为：%d\n",i);
		L=L->next;
		i++;
	}
	return FALSE;
}


//（9）定义删除单链表L的第i个元素的函数
Status ListDelete_L(LinkList L, int i)
{
	LinkList p = L,q;
	int j = 0;
	while (p->next&&j< i-1)
	{
		p = p->next;
		++j;
	}
	if (!(p->next)||j>i-1) return ERROR;
	q=p->next;
	p->next=q->next;
	return OK;
}

//（10）定义销毁单链表函数
void DestoryList(LinkList L)
{

	LinkList p=L, q=p->next;
	while (q!=NULL)
	{
		free(p);
		p=q;
		q=p->next;
	}
	free(p);
}


//主函数
void main()
{
	LinkList  h;                        //定义单链表变量h
	InitList_L(h);                      //（1）调用初始化单链表函数
	printf("（2）插入元素a,b,c,d,e已完成！\n");
	ListInsert_L(h,1,'a');              //（2）调用在表尾插入元素函数，插入元素a
	ListInsert_L(h,2,'b');              //（2）调用在表尾插入元素函数，插入元素b
	ListInsert_L(h,3,'c');              //（2）调用在表尾插入元素函数，插入元素c
	ListInsert_L(h,4,'d');              //（2）调用在表尾插入元素函数，插入元素d
	ListInsert_L(h,5,'e');              //（2）调用在表尾插入元素函数，插入元素e
	printf("（3）输出插入元素a,b,c,d,e后的单链表如下：\n");
	DispList_L(h);                      //（3）调用输出单链表函数
	listLength_L(h);                    //（4）调用输出单链表长度的函数
	Is_Empty(h);                        //（5）调用判断单链表是否为空的函数
	printf("（6）输出单链表L的第4个元素已完成！\n");
	GetElem_L(h,4,'e');                 //（6）调用输出单链表L的第i个元素，输出单链表L的第4个元素
	printf("（7）输出元素c的位置已完成！\n");
	LocateElem_L(h,'c');                //（7）调用输出单链表中某元素的位置的函数，输出元素c的位置
	printf("（8）在第3个位置上插入元素f，之后输出单链表L已完成！\n");
	ListInsert_L(h,3,'f');              //（8）调用（2）插入元素函数，将元素f插入到单链表的第3个位置
	DispList_L(h);                      //调用（3）中输出单链表函数
	printf("（9）删除L的第2个元素，之后输出单链表L已完成！\n");
	ListDelete_L(h,2);                  //（9）调用删除单链表L的第i个元素的函数，删除单链表L的第2个元素
	DispList_L(h);                      //调用（3）中输出单链表函数
	printf("（10）销毁单链表已完成！\n");
	DestoryList(h);                     //（10）调用销毁单链表函数
}


