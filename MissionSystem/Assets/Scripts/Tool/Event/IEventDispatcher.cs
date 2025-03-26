namespace ThGold.Event
{
    /// <summary>
    /// IEventDispatcher �¼��ַ��ӿڣ�����Ƴ��ַ�
    /// </summary>
    public interface IEventDispatcher
    {
        /// <summary>
        /// ����¼�����
        /// </summary>
        /// <returns><c>true</c>, ����¼��ɹ��������� <c>false</c> ʧ�� </returns>
        /// <param name="aEventType_string">A event type_string.</param>
        /// <param name="aEventDelegate">һ���¼�ί��</param>
        bool AddEventListener(string aEventType_string, EventDelegate aEventDelegate);

        /// <summary>
        /// ����¼�����
        /// </summary>
        /// <returns><c>true</c>, ����¼��ɹ�������, <c>false</c> ʧ�� </returns>
        /// <param name="aEventType_string">A event type_string.</param>
        /// <param name="aEventDelegate">һ���¼�ί��</param>
        /// <param name="eventDispatcherAddMode">Event dispatcher add mode.</param>
        bool AddEventListener(string aEventType_string, EventDelegate aEventDelegate, EventDispatcherAddMode eventDispatcherAddMode);


        /// <summary>
        /// �Ƿ�������¼�����
        /// </summary>
        /// <returns><c>true</c>, ��������Ѿ�����, <c>false</c> û��.</returns>
        /// <param name="aEventType_string">A event type_string.</param>
        /// <param name="aEventDelegate">һ���¼�ί��</param>
        bool hasEventListener(string aEventType_string, EventDelegate aEventDelegate);

        /// <summary>
        /// �Ƴ��¼�����
        /// </summary>
        /// <returns><c>true</c>, ���ɹ��Ƴ� <c>false</c> ʧ��</returns>
        /// <param name="aEventType_string">A event type_string.</param>
        /// <param name="aEventDelegate">һ���¼�ί��</param>
        bool RemoveEventListener(string aEventType_string, EventDelegate aEventDelegate);

        /// <summary>
        /// �Ƴ������¼�����
        /// </summary>
        /// <returns><c>true</c>, �ɹ�, <c>false</c> ʧ�� </returns>
        bool RemoveAllEventListeners();

        /// <summary>
        /// �ַ��㲥�¼�
        /// </summary>
        /// <returns><c>true</c>, �ɹ�, <c>false</c> ʧ��</returns>
        /// <param name="aIEvent">A I event.</param>
        bool DispatchEvent(IEvent aIEvent);
    }
}