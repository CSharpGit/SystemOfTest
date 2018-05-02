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

//��1�������ʼ����������
Status InitList_L(LinkList &L)
 {
	L=(LinkList)malloc(sizeof(LNode)) ;
	L->next=NULL;
	printf("��1����ʼ������������ɣ�\n");
	return OK;
}


//��2�������ڱ�β����Ԫ�غ���
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


//��3�����������������
void DispList_L(LinkList L) 
{
	LinkList p=L->next;
	while (p!=NULL)
	{
		printf("%c\n",p->data);
		p=p->next;
	}

}


//��4����������������ȵĺ���
Status listLength_L(LinkList &L)  
{  
       LinkList p=L->next;  
       int length=0;  
       while(p!=NULL)  
       {  
              length++;  
              p=p->next;  
       }  
	   printf("��4�����������������ɣ�\nlistLength=%d\n",length);
       return length;
}


//��5�������жϵ������Ƿ�Ϊ�յĺ���
bool Is_Empty(LinkList &L)
{
    printf("��5���жϵ�����L�Ƿ�Ϊ��,����ɣ�\n");
	if (L->next == NULL)
    {
        printf ("���жϣ�����Ϊ�գ�\n");
        return true;
    } 
	else
    {
		printf ("���жϣ�����Ϊ�գ�\n");
        return false;
    }
}


//��6���������������L�ĵ�i��Ԫ��
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
	printf("��%d��Ԫ��Ϊ%c\n",i,e);
	return OK;

}

//��7�����������������ĳԪ�ص�λ�õĺ���
Status LocateElem_L(LinkList L,ElemType e)
{
	int i=1;
	if(!L)
		return ERROR;
	L=L->next;
    while(L)
	{
		if(L->data==e)
			printf("������L��Ԫ��c��λ��Ϊ��%d\n",i);
		L=L->next;
		i++;
	}
	return FALSE;
}


//��9������ɾ��������L�ĵ�i��Ԫ�صĺ���
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

//��10���������ٵ�������
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


//������
void main()
{
	LinkList  h;                        //���嵥�������h
	InitList_L(h);                      //��1�����ó�ʼ����������
	printf("��2������Ԫ��a,b,c,d,e����ɣ�\n");
	ListInsert_L(h,1,'a');              //��2�������ڱ�β����Ԫ�غ���������Ԫ��a
	ListInsert_L(h,2,'b');              //��2�������ڱ�β����Ԫ�غ���������Ԫ��b
	ListInsert_L(h,3,'c');              //��2�������ڱ�β����Ԫ�غ���������Ԫ��c
	ListInsert_L(h,4,'d');              //��2�������ڱ�β����Ԫ�غ���������Ԫ��d
	ListInsert_L(h,5,'e');              //��2�������ڱ�β����Ԫ�غ���������Ԫ��e
	printf("��3���������Ԫ��a,b,c,d,e��ĵ��������£�\n");
	DispList_L(h);                      //��3�����������������
	listLength_L(h);                    //��4����������������ȵĺ���
	Is_Empty(h);                        //��5�������жϵ������Ƿ�Ϊ�յĺ���
	printf("��6�����������L�ĵ�4��Ԫ������ɣ�\n");
	GetElem_L(h,4,'e');                 //��6���������������L�ĵ�i��Ԫ�أ����������L�ĵ�4��Ԫ��
	printf("��7�����Ԫ��c��λ������ɣ�\n");
	LocateElem_L(h,'c');                //��7�����������������ĳԪ�ص�λ�õĺ��������Ԫ��c��λ��
	printf("��8���ڵ�3��λ���ϲ���Ԫ��f��֮�����������L����ɣ�\n");
	ListInsert_L(h,3,'f');              //��8�����ã�2������Ԫ�غ�������Ԫ��f���뵽������ĵ�3��λ��
	DispList_L(h);                      //���ã�3���������������
	printf("��9��ɾ��L�ĵ�2��Ԫ�أ�֮�����������L����ɣ�\n");
	ListDelete_L(h,2);                  //��9������ɾ��������L�ĵ�i��Ԫ�صĺ�����ɾ��������L�ĵ�2��Ԫ��
	DispList_L(h);                      //���ã�3���������������
	printf("��10�����ٵ���������ɣ�\n");
	DestoryList(h);                     //��10���������ٵ�������
}


